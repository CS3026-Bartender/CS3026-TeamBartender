using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoSimController : MonoBehaviour
{
    [Header("Simulation Settings")]
    [SerializeField] private float maxSimulationTime = 30f;
    [SerializeField] private CustomerController customerController;

    [Header("Debug")]
    [SerializeField] private bool isSimulationRunning = false;
    [SerializeField] private float currentSimulationTime = 0f;
    [SerializeField] private List<Drink> activeDrinks = new List<Drink>();

    // Singleton pattern
    private static AutoSimController instance;
    public static AutoSimController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartSimulation();
    }

    private void Update()
    {
        if (isSimulationRunning)
        {
            // Update simulation time
            currentSimulationTime += Time.deltaTime;

            // Check if simulation should end
            if (currentSimulationTime >= maxSimulationTime)
            {
                EndSimulation();
                return;
            }

            // Update all active drink cooldowns
            UpdateDrinkCooldowns();
        }
    }

    // Start the auto simulation
    public void StartSimulation()
    {
        if (isSimulationRunning)
        {
            Debug.LogWarning("Simulation is already running!");
            return;
        }

        if (customerController == null || customerController.SelectedCustomer == null)
        {
            Debug.LogError("Cannot start simulation: No target customer assigned!");
            return;
        }

        // Reset simulation values
        currentSimulationTime = 0f;
        activeDrinks.Clear();
        isSimulationRunning = true;

        // Initialize drinks from DrinkManager
        if (DrinkManager.Instance != null && DrinkManager.Instance.IsInitialized)
        {
            List<Drink> equippedDrinks = DrinkManager.Instance.GetEquippedDrinks();
            foreach (Drink drink in equippedDrinks)
            {
                // Initialize the cooldownRemaining value
                Drink drinkCopy = drink;
                drinkCopy.cooldownRemaining = drink.cooldownseconds;

                // Add all equipped drinks to active drinks
                AddDrinkToSimulation(drinkCopy);
            }
        }
        else
        {
            Debug.LogError("Cannot start simulation: DrinkManager not initialized!");
            isSimulationRunning = false;
        }

        Debug.Log("Auto simulation started!");
    }

    // End the auto simulation
    public void EndSimulation()
    {
        if (!isSimulationRunning) return;

        isSimulationRunning = false;
        Debug.Log($"Simulation ended after {currentSimulationTime:F1} seconds");

        // Any cleanup or final satisfaction calculations can go here

        // Optional: Event that other systems can listen to
        // OnSimulationEnded?.Invoke(function);
    }

    // Add a drink to the active simulation
    public void AddDrinkToSimulation(Drink drink)
    {
        if (!isSimulationRunning) return;

        // Add drink to active drinks
        activeDrinks.Add(drink);

        // Immediately apply first satisfaction increase
        ApplyDrinkEffect(drink);

        Debug.Log($"Added drink {drink.name} to simulation with cooldown {drink.cooldownseconds}s");
    }

    // Update all active drink cooldowns
    private void UpdateDrinkCooldowns()
    {
        for (int i = activeDrinks.Count - 1; i >= 0; i--)
        {
            // Get the drink
            Drink drink = activeDrinks[i];

            // Reduce remaining cooldown
            drink.cooldownRemaining -= Time.deltaTime;

            // Important: Update the drink in the list immediately after modifying it
            activeDrinks[i] = drink;

            // Check if cooldown has elapsed
            if (drink.cooldownRemaining <= 0)
            {
                // Apply the drink effect
                ApplyDrinkEffect(drink);

                // Reset cooldown
                drink.cooldownRemaining = drink.cooldownseconds;

                // Update the drink in the list again after resetting cooldown
                activeDrinks[i] = drink;

             
            }
        }
    }

    // Apply a drink's effect to the target customer
    private void ApplyDrinkEffect(Drink drink)
    {
        if (customerController != null && customerController.SelectedCustomer != null)
        {
            // Increase customer satisfaction based on drink's potency
            // Get the Customer component from the target customer GameObject
            customer customerScript = customerController.SelectedCustomer.GetComponent<customer>();
            if (customerScript != null)
            {
                float satisfaction = CalculateSatisfaction(drink, customerScript);
                customerScript.IncreaseSatisfaction(drink.potency);
                Debug.Log($"Applied {drink.name} effect: +{drink.potency} satisfaction");
            }
            else
            {
                Debug.LogError("Customer script component not found on target customer!");
            }
        }
    }


    float CalculateSatisfaction(Drink drink, customer customer)
    {
        float satisfaction = 0f;

        switch (customer.currentType)
        {
            case customer.CustomerType.Regular:  // Type 0
                satisfaction = drink.potency + drink.richness;
                break;

            case customer.CustomerType.Premium:  // Type 1
                satisfaction = drink.potency + drink.fruityness;
                break;

            case customer.CustomerType.VIP:      // Type 2
                satisfaction = drink.potency + drink.bitterness;
                break;

            default:
                Debug.LogWarning("Unknown customer type encountered.");
                satisfaction = drink.potency;  // Fallback to just potency
                break;
        }

        return Mathf.Max(0f, satisfaction);
    }
}