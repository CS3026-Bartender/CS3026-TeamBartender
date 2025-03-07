using UnityEngine;
using System.Collections.Generic;

public class DrinkManager : MonoBehaviour
{
    [SerializeField] private DrinkDatabase drinkDatabase;
    [SerializeField] private int maxEquippedDrinks = 3;

  
    [SerializeField] private string[] equippedDrinks;

    private static DrinkManager instance;
    private List<Drink> allDrinks = new List<Drink>();
    private bool isInitialized = false;

    private void Awake()
    {
        // Simple singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDrinks();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        // Ensure equippedDrinks array is initialized with the correct size in the editor
        if (equippedDrinks == null || equippedDrinks.Length != maxEquippedDrinks)
        {
            equippedDrinks = new string[maxEquippedDrinks];
        }
    }

    // Load drinks from the database
    private void InitializeDrinks()
    {
        if (drinkDatabase == null)
        {
            drinkDatabase = Resources.Load<DrinkDatabase>("DrinkDatabase");
        }

        if (drinkDatabase != null)
        {
            allDrinks = new List<Drink>(drinkDatabase.drinks);
            Debug.Log($"Loaded {allDrinks.Count} drinks from database");

            // Initialize equipped drinks array if it hasn't been already
            if (equippedDrinks == null || equippedDrinks.Length != maxEquippedDrinks)
            {
                equippedDrinks = new string[maxEquippedDrinks];
            }

            isInitialized = true;
        }
        else
        {
            Debug.LogError("Failed to load DrinkDatabase!");
        }
    }

    // Static accessor for the singleton
    public static DrinkManager Instance
    {
        get { return instance; }
    }

    // Public property to check if initialization is complete
    public bool IsInitialized
    {
        get { return isInitialized; }
    }

    // Get all drinks
    public List<Drink> GetAllDrinks()
    {
        return allDrinks;
    }

    // Get drink by name
    public Drink GetDrink(string drinkName)
    {
        return allDrinks.Find(d => d.name == drinkName);
    }

    // Get all equipped drinks (returns only valid drinks, not empty slots)
    public List<Drink> GetEquippedDrinks()
    {
        List<Drink> drinks = new List<Drink>();

        foreach (string drinkName in equippedDrinks)
        {
            if (!string.IsNullOrEmpty(drinkName))
            {
                Drink drink = GetDrink(drinkName);
                if (drink.name != null) // Check if it's a valid drink
                {
                    drinks.Add(drink);
                }
            }
        }

        return drinks;
    }

    // Add a drink to equipped drinks if there's room
    public bool AddDrink(string drinkName)
    {
        // Verify the drink exists
        Drink drink = GetDrink(drinkName);
        if (string.IsNullOrEmpty(drink.name))
        {
            Debug.LogWarning($"Cannot equip drink '{drinkName}': Drink not found in database");
            return false;
        }

        // Check if already equipped
        for (int i = 0; i < equippedDrinks.Length; i++)
        {
            if (equippedDrinks[i] == drinkName)
            {
                Debug.Log($"Drink '{drinkName}' is already equipped");
                return true;
            }
        }

        // Find an empty slot
        for (int i = 0; i < equippedDrinks.Length; i++)
        {
            if (string.IsNullOrEmpty(equippedDrinks[i]))
            {
                equippedDrinks[i] = drinkName;
                Debug.Log($"Equipped drink '{drinkName}' in slot {i}");
                return true;
            }
        }

        Debug.LogWarning("Cannot equip drink: No empty slots available");
        return false;
    }

    // Remove a drink from equipped drinks
    public bool RemoveDrink(string drinkName)
    {
        for (int i = 0; i < equippedDrinks.Length; i++)
        {
            if (equippedDrinks[i] == drinkName)
            {
                equippedDrinks[i] = null;
                Debug.Log($"Removed drink '{drinkName}' from equipped drinks");
                return true;
            }
        }

        Debug.LogWarning($"Cannot remove drink '{drinkName}': Not currently equipped");
        return false;
    }

    // Get the max number of drinks that can be equipped
    public int GetMaxEquippedDrinks()
    {
        return maxEquippedDrinks;
    }

    // Set the max number of equipped drinks (will resize the array)
    public void SetMaxEquippedDrinks(int max)
    {
        if (max < 1)
        {
            Debug.LogWarning("Max equipped drinks must be at least 1");
            max = 1;
        }

        // Create a new array with the new size
        string[] newEquippedDrinks = new string[max];

        // Copy over existing drinks (up to the new max)
        for (int i = 0; i < Mathf.Min(equippedDrinks.Length, max); i++)
        {
            newEquippedDrinks[i] = equippedDrinks[i];
        }

        equippedDrinks = newEquippedDrinks;
        maxEquippedDrinks = max;
    }
}