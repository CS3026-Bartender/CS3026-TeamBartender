using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;


    // Variables for targeted Customer
    private GameObject SelectedCustomer;
    [SerializeField] private Button[] CustomerTargetButtons;
    [SerializeField] private Transform Target;

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

        // Set up targeted customer
        SelectedCustomer = customerSlots[0];
        Target.position = spawnPoints[0].position;

        // Setup buttons to switch target. 
        CustomerTargetButtons[0].onClick.AddListener(() => SetTargetToCustomer(0));
        CustomerTargetButtons[1].onClick.AddListener(() => SetTargetToCustomer(1));
        CustomerTargetButtons[2].onClick.AddListener(() => SetTargetToCustomer(2));

    }


    private void SetTargetToCustomer(int index)
    {
        if (customerSlots[index] != null)
        {
            SelectedCustomer = customerSlots[index];
            Target.position = spawnPoints[index].position;
            
        }
    }

    private void OnDestroy()
    {
        CustomerTargetButtons[0].onClick.RemoveListener(() => SetTargetToCustomer(0));
        CustomerTargetButtons[1].onClick.RemoveListener(() => SetTargetToCustomer(1));
        CustomerTargetButtons[2].onClick.RemoveListener(() => SetTargetToCustomer(2));


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
    public void RemoveCustomer(GameObject Customer)
    {
      
       

       for (int i=0; i< 3; i++)
        {
            if (customerSlots[i] == Customer)
            {

                Destroy(customerSlots[i]);
                customerSlots[i] = null;
                Debug.Log("Customer removed.");
            }
            return;
        }
       
      
            Debug.Log("Customer Couldnt be found");
        
    }




    //  Stop the spawning process
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // Resume or start the spawning process
    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnCustomersRoutine());
        }
    }
}