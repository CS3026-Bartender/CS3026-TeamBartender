using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;

    // Keep track of customers in each slot (null = empty)
    private GameObject[] customerSlots = new GameObject[3];

    // Coroutine reference for customer spawning
    private Coroutine spawnCoroutine;

    private void Start()
    {
        // Start the spawning process
        spawnCoroutine = StartCoroutine(SpawnCustomersRoutine());

        // Spawn the first customer immediately
        SpawnCustomer();
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

        // Spawn a customer at the appropriate position
        GameObject newCustomer = Instantiate(customerPrefab, spawnPoints[emptySlotIndex].position, Quaternion.identity);

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
    public void RemoveCustomer(int slotIndex)
    {
        // Validate the slot index
        if (slotIndex < 0 || slotIndex >= customerSlots.Length)
        {
            Debug.LogError("Invalid slot index: " + slotIndex);
            return;
        }

        // Check if there's a customer in the specified slot
        if (customerSlots[slotIndex] != null)
        {
            // Destroy the customer game object
            Destroy(customerSlots[slotIndex]);

            // Clear the slot reference
            customerSlots[slotIndex] = null;

            Debug.Log("Customer removed from slot: " + slotIndex);
        }
        else
        {
            Debug.Log("No customer in slot: " + slotIndex);
        }
    }

    // Overload to remove the first available customer (from lowest index)
    public void RemoveCustomer()
    {
        for (int i = 0; i < customerSlots.Length; i++)
        {
            if (customerSlots[i] != null)
            {
                RemoveCustomer(i);
                return;
            }
        }

        Debug.Log("No customers to remove.");
    }

    // Optional: Stop the spawning process
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // Optional: Resume or start the spawning process
    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnCustomersRoutine());
        }
    }
}