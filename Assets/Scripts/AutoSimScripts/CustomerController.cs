using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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


    [Header("TimerDisplayControl")]

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private string postfix = "s";

    // Variables for targeted Customer
    private GameObject currentlyServingCustomer;
    [SerializeField] private Transform Target;
    private SpriteRenderer targetSpriteRenderer;

    // Keep track of customers in each slot (null = empty)
    private GameObject[] customerSlots = new GameObject[3];

    // Queue for customer service
    private Queue<GameObject> customerQueue = new Queue<GameObject>();
    private bool isProcessingQueue = false;

    // Coroutine reference for customer spawning
    private Coroutine spawnCoroutine;
    private Coroutine simulationCoroutine;
    private Coroutine processQueueCoroutine;

    // Cooldown tracking
    private bool isInCooldown = false;
    private Coroutine cooldownCoroutine;

    public Slider CooldownBarSlider;



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

    }

    private void Start()
    {
        // Start the spawning process
        spawnCoroutine = StartCoroutine(SpawnCustomersRoutine());

        // Start the simulation timer
        simulationCoroutine = StartCoroutine(SimulationTimerRoutine());

        // Start processing the queue
        processQueueCoroutine = StartCoroutine(ProcessCustomerQueueRoutine());
    }

    // Modified coroutine to handle the simulation duration with earnings overlay
    private IEnumerator SimulationTimerRoutine()
    {
        Debug.Log($"Simulation will run for {simulationDuration} seconds before showing earnings");

        // Wait for the specified duration
        for (int i = 0; i < simulationDuration; i++) {
            yield return new WaitForSeconds(1.0f);
            if (timerText != null) {
                timerText.text = $"{simulationDuration - i - 1}";
            }

        }
        //yield return new WaitForSeconds(simulationDuration);

        // Time's up! Stop spawning and clear all customers
        StopSpawning();
        ClearAllCustomers();

        // Wait a moment to let any animations finish if needed
        yield return new WaitForSeconds(0.5f);

        // Show the earnings overlay 
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

    // Method to remove all customers at once
    private void ClearAllCustomers()
    {
        Debug.Log("Clearing all customers");

        // Clear the queue
        customerQueue.Clear();
        currentlyServingCustomer = null;

        // Stop processing the queue
        if (processQueueCoroutine != null)
        {
            StopCoroutine(processQueueCoroutine);
            processQueueCoroutine = null;
        }

        for (int i = 0; i < customerSlots.Length; i++)
        {
            if (customerSlots[i] != null)
            {
                Destroy(customerSlots[i]);
                customerSlots[i] = null;
            }
        }

        // Hide target
        if (targetSpriteRenderer != null)
        {
            targetSpriteRenderer.enabled = false;
        }

        isProcessingQueue = false;
    }

    // Public method to add a customer to the queue
    public void AddCustomerToQueue(GameObject customer)
    {
        if (customer != null && !customerQueue.Contains(customer))
        {
            customerQueue.Enqueue(customer);
            Debug.Log("Customer added to queue. Queue size: " + customerQueue.Count);
        }
    }

    // Coroutine to process customers in the queue
    private IEnumerator ProcessCustomerQueueRoutine()
    {
        while (true)
        {
            // If we're not already serving a customer and there are customers in the queue
            if (!isProcessingQueue && customerQueue.Count > 0)
            {
                isProcessingQueue = true;

                // Get the next customer from the queue
                GameObject nextCustomer = customerQueue.Dequeue();

                // Check if the customer still exists and can be served
                if (nextCustomer != null)
                {
                    Customer customer = nextCustomer.GetComponent<Customer>();

                    if (customer != null && customer.CanBeServed)
                    {
                        currentlyServingCustomer = nextCustomer;

                        // Find which slot the customer is in to position the target
                        for (int i = 0; i < customerSlots.Length; i++)
                        {
                            if (customerSlots[i] == nextCustomer)
                            {
                                // Position and show the target
                                Target.position = spawnPoints[i].position;
                                if (targetSpriteRenderer != null)
                                {
                                    targetSpriteRenderer.enabled = true;
                                }

                                StartCoroutine(ServeCustomer(customer));
                                break;
                            }
                        }
                    }
                    else
                    {
                        // If the customer can't be served, move on to the next one
                        isProcessingQueue = false;
                    }
                }
                else
                {
                    // If the customer no longer exists, move on to the next one
                    isProcessingQueue = false;
                }
            }

            yield return null;
        }
    }

    private IEnumerator ServeCustomer(Customer customer)
    {

        // Get the serve time from the customer's preferred drink
        float serveTime = customer.preferredDrink.GetCalculatedServeTime();

        // Call the Serve method on the customer (changes display to "Waiting for")
        customer.Serve();

        // Start cooldown
        StartCooldown(serveTime, customer);

        Debug.Log("Automatically preparing drink for " + serveTime + " seconds");

        // Wait for the cooldown to complete
        yield return new WaitForSeconds(serveTime);

        // Hide the target after serving is complete
        if (targetSpriteRenderer != null)
        {
            targetSpriteRenderer.enabled = false;
        }

        // Wait for a short pause between serving customers (0.25 seconds)
        yield return new WaitForSeconds(0.25f);

        isProcessingQueue = false;
        currentlyServingCustomer = null;
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

       

        float timeElapsed = 0f;

        CooldownBarSlider.value = timeElapsed;

        while (timeElapsed < duration)
        {
            // Update cooldown fill image if assigned
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = 1f - (timeElapsed / duration);
            }

            
            CooldownBarSlider.value = (timeElapsed / duration);
            

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Hide cooldown UI when done
        if (cooldownPanel != null)
        {
            yield return new WaitForSeconds(0.2f);
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

        isInCooldown = false;
    }

    private void OnDestroy()
    {
        // Make sure to stop all coroutines when destroyed
        StopAllCoroutines();
    }

    private IEnumerator SpawnCustomersRoutine()
    {
        while (true)
        {
            // Add variance of ±2 seconds to the spawn interval
            float randomizedInterval = spawnInterval + UnityEngine.Random.Range(-2f, 2f);
            // Make sure the interval doesn't go below 0.1
            randomizedInterval = Mathf.Max(0.1f, randomizedInterval);

            yield return new WaitForSeconds(randomizedInterval);
            SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        // Find all empty slots and store their indices
        List<int> emptySlots = new List<int>();
        for (int i = 0; i < customerSlots.Length; i++)
        {
            if (customerSlots[i] == null)
            {
                emptySlots.Add(i);
            }
        }

        // If no empty slots, don't spawn
        if (emptySlots.Count == 0)
        {
            Debug.Log("No empty slots available for spawning customers.");
            return;
        }

        // Choose a random empty slot
        int randomIndex = UnityEngine.Random.Range(0, emptySlots.Count);
        int chosenSlotIndex = emptySlots[randomIndex];

        AudioManager.Instance.PlaySound("customer_enters");

        // Spawn a customer at the randomly chosen empty slot position
        GameObject newCustomer = Instantiate(customerPrefab, spawnPoints[chosenSlotIndex].position, Quaternion.identity, customerParent);

        // Store reference to the customer in the chosen slot
        customerSlots[chosenSlotIndex] = newCustomer;

        // For debugging
        Debug.Log("Customer spawned in slot: " + chosenSlotIndex);
    }

    // Public method to remove a customer
    public void RemoveCustomer(GameObject Customer)
    {
        for (int i = 0; i < customerSlots.Length; i++)
        {
            if (customerSlots[i] == Customer)
            {
                // Check if the removed customer was being served
                bool wasBeingServed = (Customer == currentlyServingCustomer);

                Destroy(customerSlots[i]);
                customerSlots[i] = null;
                Debug.Log("Customer removed.");

                // If the removed customer was being served, clear reference
                if (wasBeingServed)
                {
                    currentlyServingCustomer = null;

                    // Hide the target
                    if (targetSpriteRenderer != null)
                    {
                        targetSpriteRenderer.enabled = false;
                    }
                }

                return;
            }
        }

        Debug.Log("Customer couldn't be found");
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