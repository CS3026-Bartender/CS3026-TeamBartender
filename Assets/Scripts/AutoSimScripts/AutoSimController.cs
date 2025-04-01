using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AutoSimController : MonoBehaviour
{
    [Header("Simulation Settings")]
    [SerializeField] private float maxSimulationTime = 30f;
    [SerializeField] private CustomerController customerController;
   
   


    [Header("Debug")]
    [SerializeField] private bool isSimulationRunning = false;
    [SerializeField] private float currentSimulationTime = 0f;
    private List<Ingredient> activeSpirits = new List<Ingredient>();

    // Singleton pattern
    private static AutoSimController instance;
    public static AutoSimController Instance
    {
        get { return instance; }
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
        activeSpirits.Clear();
        isSimulationRunning = true;

        // Initialize drinks from DrinkManager
     
        
            // Get all equipped drinks with their ingredients applied
            List<Drink> equippedDrinks = DrinkData.Instance.GetAllDrinksAsList();

            if (equippedDrinks.Count == 0)
            {
                Debug.LogWarning("No equipped drinks found! Please equip drinks before starting simulation.");
                // Don't stop the simulation, but log a warning
            }

            Debug.Log($"Found {equippedDrinks.Count} equipped drinks for simulation");

            foreach (Drink drink in equippedDrinks)
            {
                string spiritidstring = drink.GetSpiritID();
                AddSpiritToSimulation(IngredientData.GetIngValue(spiritidstring));
            }
      

        Debug.Log("Auto simulation started!");
    }

    // End the auto simulation
    public void EndSimulation()
    {
        if (!isSimulationRunning) return;

        isSimulationRunning = false;
        Debug.Log($"Simulation ended after {currentSimulationTime:F1} seconds");

        SceneManager.LoadScene("Shop_Scene");

        // Optional: Event that other systems can listen to
        // OnSimulationEnded?.Invoke(function);
    }

    // Add a drink to the active simulation
    public void AddSpiritToSimulation(Ingredient spirit)
    {
        if (!isSimulationRunning)
        {
            Debug.LogWarning("Cannot add drink: Simulation is not running");
            return;
        }

        // Add drink to active drinks
        activeSpirits.Add(spirit);

        // Immediately apply first satisfaction increase
        ApplyDrinkEffect(spirit);

    
    }

    // Update all active drink cooldowns
    private void UpdateDrinkCooldowns()
    {
        for (int i = activeSpirits.Count - 1; i >= 0; i--)
        {
          
                Ingredient spirits = activeSpirits[i];
            if (spirits is Spirit spirit)
            {
                // Reduce remaining cooldown
                spirit.ServeTimeRemaining -= Time.deltaTime;

            // Important: Update the drink in the list immediately after modifying it
            activeSpirits[i] = spirit;

                // Check if cooldown has elapsed
                if (spirit.ServeTimeRemaining <= 0)
                {
                    // Apply the drink effect
                    ApplyDrinkEffect(spirit);

                    // Reset cooldown
                    spirit.ServeTimeRemaining = spirit.ServeTime;

                    // Update the drink in the list again after resetting cooldown
                    activeSpirits[i] = spirit;

                    Debug.Log($"Applied {spirit.DisplayName} effect again after cooldown elapsed");
                }
            }
        }
    }

    // Apply a drink's effect to the target customer
    private void ApplyDrinkEffect(Ingredient spirits)
    {
        if (customerController == null)
        {
            Debug.LogError("Customer controller is null!");
            return;
        }

        if (customerController.SelectedCustomer == null)
        {
            Debug.LogError("No selected customer!");
            return;
        }

        // Get the Customer component from the target customer GameObject
        customer customerScript = customerController.SelectedCustomer.GetComponent<customer>();
        if (customerScript != null)
        {

            if (spirits is Spirit spirit)
            {
                customerScript.IncreaseSatisfaction(spirit.Potentcy);

                // Log detailed information about the drink effect
                Debug.Log($"Applied '{spirit.DisplayName}' effect to {customerScript.name} (Type: {customerScript.currentType})" +
                          $"\n  Satisfaction increase: {spirit.Potentcy}");

            }
        }
        else
        {
            Debug.LogError("Customer script component not found on target customer!");
        }
    }

  
}