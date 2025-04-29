using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomerController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float simulationDuration = 180f; // Duration in seconds before switching to shop scene
    [SerializeField] private Transform customerParent;

    [Header("Scene Settings")]
    [SerializeField] private EarningsOverlay earningsOverlay; // Reference to the earnings overlay

    [Header("Cooldown Settings")]
    [SerializeField] private Image cooldownImage; // Reference to UI cooldown image WIP
    [SerializeField] private GameObject cooldownPanel; // Reference to cooldown panel WIP

    // Variables for targeted Customer
    public GameObject SelectedCustomer;
    [SerializeField] private Button[] CustomerTargetButtons;
    [SerializeField] private Transform Target;
    private SpriteRenderer targetSpriteRenderer;

    // Keep track of customers in each slot (null = empty)
    private GameObject[] customerSlots = new GameObject[3];

    // Coroutine reference for customer spawning
    private Coroutine spawnCoroutine;
    private Coroutine simulationCoroutine;

    // Cooldown tracking
    private bool isInCooldown = false;
    private Coroutine cooldownCoroutine;

    private void Awake()
    {
        // Get the sprite renderer component of the target
        targetSpriteRenderer = Target.GetComponent<SpriteRenderer>();

        // Hide the target initially
        if (targetSpriteRenderer != null)
        {
            targetSpriteRenderer.enabled = false;
        }

        SpawnCustomer();

        // Initialize cooldown UI if assigned
        if (cooldownPanel != null)
        {
            cooldownPanel.SetActive(false);
        }
    }

    private void Start()
    {
        // Start the spawning process
        spawnCoroutine = StartCoroutine(SpawnCustomersRoutine());

        // Set up buttons to switch target
        CustomerTargetButtons[0].onClick.AddListener(() => TrySelectCustomer(0));
        CustomerTargetButtons[1].onClick.AddListener(() => TrySelectCustomer(1));
        CustomerTargetButtons[2].onClick.AddListener(() => TrySelectCustomer(2));

        // Start the simulation timer
        simulationCoroutine = StartCoroutine(SimulationTimerRoutine());
    }

    private void Update()
    {
        // Check for keyboard input to select customers
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            TrySelectCustomer(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            TrySelectCustomer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            TrySelectCustomer(2);
        }
    }

    // Modified coroutine to handle the simulation duration with earnings overlay
    private IEnumerator SimulationTimerRoutine()
    {
        Debug.Log($"Simulation will run for {simulationDuration} seconds before showing earnings");

        // Wait for the specified duration
        yield return new WaitForSeconds(simulationDuration);

        // Time's up! Stop spawning and clear all customers
        StopSpawning();
        ClearAllCustomers();

        // Wait a moment to let any animations finish if needed
        yield return new WaitForSeconds(0.5f);

        // Show the earnings overlay instead of immediately switching scenes
        if (earningsOverlay != null)
        {
            Debug.Log("Simulation ended. Showing earnings overlay.");
            earningsOverlay.ShowEarningsOverlay();
        }
        else
        {
            Debug.LogWarning("Earnings overlay not assigned! Switching directly to shop scene.");
            SceneManager.LoadScene("Shop_Scene");
        }
    }

    // New method to remove all customers at once
    private void ClearAllCustomers()
    {
        Debug.Log("Clearing all customers");

        for (int i = 0; i < customerSlots.Length; i++)
        {
            if (customerSlots[i] != null)
            {
                Destroy(customerSlots[i]);
                customerSlots[i] = null;
            }
        }

        // Make sure the selected customer is cleared and target is hidden
        SelectedCustomer = null;
        if (targetSpriteRenderer != null)
        {
            targetSpriteRenderer.enabled = false;
        }
    }

    private void TrySelectCustomer(int index)
    {
        // Don't allow selection during cooldown
        if (isInCooldown)
        {
            Debug.Log("Cannot select customer during cooldown");
            return;
        }

        // Check if there's a customer in the slot
        if (customerSlots[index] != null)
        {
            // Check if the customer can be served
            Customer customer = customerSlots[index].GetComponent<Customer>();

            if (customer != null && customer.CanBeServed)
            {
                // Select the customer
                SelectedCustomer = customerSlots[index];
                Target.position = spawnPoints[index].position;

                // Ensure target sprite is visible when a customer is selected
                if (targetSpriteRenderer != null)
                {
                    targetSpriteRenderer.enabled = true;
                }

                // Prepare the customer's order
                PrepareOrderForSelectedCustomer();
            }
            else
            {
                Debug.Log("Customer cannot be served at this time");
            }
        }
    }

    private void PrepareOrderForSelectedCustomer()
    {
        if (SelectedCustomer == null) return;

        Customer customer = SelectedCustomer.GetComponent<Customer>();
        if (customer != null)
        {
            // Get the serve time from the customer's preferred drink
            float serveTime = customer.preferredDrink.GetCalculatedServeTime();

            // Call the Serve method on the customer (only changes display to "Preparing")
            customer.Serve();

            // Start cooldown
            StartCooldown(serveTime, customer);

            Debug.Log("Preparing drink for " + serveTime + " seconds");
        }
    }

    private void StartCooldown(float duration, Customer customer)
    {
        // Stop any existing cooldown
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }

        // Start the new cooldown
        cooldownCoroutine = StartCoroutine(CooldownRoutine(duration, customer));
    }

    private IEnumerator CooldownRoutine(float duration, Customer customer)
    {
        isInCooldown = true;

        // Show cooldown UI if assigned
        if (cooldownPanel != null)
        {
            cooldownPanel.SetActive(true);
        }

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Update cooldown fill image if assigned
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = 1f - (timeElapsed / duration);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Hide cooldown UI when done
        if (cooldownPanel != null)
        {
            cooldownPanel.SetActive(false);
        }

        // Reset cooldown image
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 1f;
        }

        // Now that cooldown is done, actually serve the drink
        if (customer != null)
        {
            customer.ServeAfterCooldown();
            Debug.Log("Cooldown finished - Customer now enjoying drink");
        }

        // Deselect customer and hide target when cooldown is finished
        SelectedCustomer = null;
        if (targetSpriteRenderer != null)
        {
            targetSpriteRenderer.enabled = false;
        }

        isInCooldown = false;
    }

    private void OnDestroy()
    {
        CustomerTargetButtons[0].onClick.RemoveListener(() => TrySelectCustomer(0));
        CustomerTargetButtons[1].onClick.RemoveListener(() => TrySelectCustomer(1));
        CustomerTargetButtons[2].onClick.RemoveListener(() => TrySelectCustomer(2));

        // Make sure to stop all coroutines when destroyed
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        if (simulationCoroutine != null)
        {
            StopCoroutine(simulationCoroutine);
        }

        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
    }

    private IEnumerator SpawnCustomersRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        // Find the first empty slot
        int emptySlotIndex = FindEmptySlot();

        // If no empty slots, don't spawn
        if (emptySlotIndex == -1)
        {
            Debug.Log("No empty slots available for spawning customers.");
            return;
        }

        AudioManager.Instance.PlaySound("customer_enters");

        // Spawn a customer at the appropriate position
        GameObject newCustomer = Instantiate(customerPrefab, spawnPoints[emptySlotIndex].position, Quaternion.identity, customerParent);

        // Store reference to the customer in the appropriate slot
        customerSlots[emptySlotIndex] = newCustomer;

        // For debugging
        Debug.Log("Customer spawned in slot: " + emptySlotIndex);
    }

    private int FindEmptySlot()
    {
        // Check slots in order (first, second, third)
        for (int i = 0; i < customerSlots.Length; i++)
        {
            if (customerSlots[i] == null)
            {
                return i;
            }
        }

        // No empty slots found
        return -1;
    }

    // Public method to remove a customer (can be called from UI buttons, events, etc.)
    public void RemoveCustomer(GameObject Customer)
    {
        for (int i = 0; i < 3; i++)
        {
            if (customerSlots[i] == Customer)
            {
                // Check if the removed customer was the selected one
                bool wasSelected = (Customer == SelectedCustomer);

                Destroy(customerSlots[i]);
                customerSlots[i] = null;
                Debug.Log("Customer removed.");

                // If the removed customer was selected, set selected to null and hide the target
                if (wasSelected)
                {
                    SelectedCustomer = null;

                    // Hide the target sprite renderer
                    if (targetSpriteRenderer != null)
                    {
                        targetSpriteRenderer.enabled = false;
                    }
                }

                return;
            }
        }

        Debug.Log("Customer Couldn't be found");
    }

    // Stop the spawning process
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
            Debug.Log("Customer spawning stopped");
        }
    }

    // Resume or start the spawning process
    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnCustomersRoutine());
            Debug.Log("Customer spawning started");
        }
    }

    // Method to manually end the simulation early if needed
    public void EndSimulationEarly()
    {
        if (simulationCoroutine != null)
        {
            StopCoroutine(simulationCoroutine);
        }

        StopSpawning();
        ClearAllCustomers();

        // Show earnings overlay instead of direct scene switch
        if (earningsOverlay != null)
        {
            earningsOverlay.ShowEarningsOverlay();
        }
        else
        {
            SceneManager.LoadScene("Shop_Scene");
        }
    }
}