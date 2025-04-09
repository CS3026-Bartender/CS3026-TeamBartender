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
    private List<Drink> Drinks = new List<Drink>();

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
        Drinks.Clear();
        isSimulationRunning = true;

        // Initialize drinks from DrinkManager
     
        
            // Get all equipped drinks with their ingredients applied
             Drinks = DrinkData.Instance.GetAllDrinksAsList();

            if (Drinks.Count == 0)
            {
                Debug.LogWarning("No equipped drinks found! Please equip drinks before starting simulation.");
                // Don't stop the simulation, but log a warning
            }

            Debug.Log($"Found {Drinks.Count} equipped drinks for simulation");

           
      

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
   

    // Update all active drink cooldowns
    
    }

  
