using UnityEngine;
using System.Collections.Generic;

public class DrinkManager : MonoBehaviour
{
    [SerializeField] private DrinkDatabase drinkDatabase;
    [SerializeField] private int maxEquippedDrinks = 3;

    // Replace string array with EquippedDrink array
    [SerializeField] private EquippedDrink[] equippedDrinks;

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
            equippedDrinks = new EquippedDrink[maxEquippedDrinks];
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
                equippedDrinks = new EquippedDrink[maxEquippedDrinks];
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

        foreach (EquippedDrink equippedDrink in equippedDrinks)
        {
            if (equippedDrink != null && !string.IsNullOrEmpty(equippedDrink.drinkName))
            {
                Drink baseDrink = GetDrink(equippedDrink.drinkName);
                if (!string.IsNullOrEmpty(baseDrink.name)) // Check if it's a valid drink
                {
                    // Get the final drink with all ingredients applied
                    // This uses Clone() to make sure we don't modify the database drink
                    Drink finalDrink = equippedDrink.GetFinalDrink(baseDrink);
                    drinks.Add(finalDrink);
                }
            }
        }

        return drinks;
    }

    // Get a specific equipped drink by slot index
    public Drink GetEquippedDrinkAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= equippedDrinks.Length || equippedDrinks[slotIndex] == null)
        {
            return new Drink(); // Return empty drink
        }

        EquippedDrink equippedDrink = equippedDrinks[slotIndex];
        Drink baseDrink = GetDrink(equippedDrink.drinkName);

        if (string.IsNullOrEmpty(baseDrink.name))
        {
            return new Drink(); // Return empty drink if base drink not found
        }

        return equippedDrink.GetFinalDrink(baseDrink);
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
            if (equippedDrinks[i] != null && equippedDrinks[i].drinkName == drinkName)
            {
                Debug.Log($"Drink '{drinkName}' is already equipped");
                return true;
            }
        }

        // Find an empty slot
        for (int i = 0; i < equippedDrinks.Length; i++)
        {
            if (equippedDrinks[i] == null)
            {
                equippedDrinks[i] = new EquippedDrink(drinkName);
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
            if (equippedDrinks[i] != null && equippedDrinks[i].drinkName == drinkName)
            {
                equippedDrinks[i] = null;
                Debug.Log($"Removed drink '{drinkName}' from equipped drinks");
                return true;
            }
        }

        Debug.LogWarning($"Cannot remove drink '{drinkName}': Not currently equipped");
        return false;
    }

    // Add an ingredient to a drink in a specific slot
    public bool AddIngredientToDrink(int slotIndex, Ingredient ingredient)
    {
        if (slotIndex < 0 || slotIndex >= equippedDrinks.Length || equippedDrinks[slotIndex] == null)
        {
            Debug.LogWarning($"Cannot add ingredient to slot {slotIndex}: No drink equipped in this slot");
            return false;
        }

        equippedDrinks[slotIndex].AddIngredient(ingredient);
        Debug.Log($"Added ingredient '{ingredient.name}' to drink in slot {slotIndex}");
        return true;
    }

    // Add an ingredient to a drink by name
    public bool AddIngredientToDrink(string drinkName, Ingredient ingredient)
    {
        for (int i = 0; i < equippedDrinks.Length; i++)
        {
            if (equippedDrinks[i] != null && equippedDrinks[i].drinkName == drinkName)
            {
                equippedDrinks[i].AddIngredient(ingredient);
                Debug.Log($"Added ingredient '{ingredient.name}' to drink '{drinkName}'");
                return true;
            }
        }

        Debug.LogWarning($"Cannot add ingredient: Drink '{drinkName}' is not equipped");
        return false;
    }

    // Remove an ingredient from a drink in a specific slot
    public bool RemoveIngredientFromDrink(int slotIndex, int ingredientIndex)
    {
        if (slotIndex < 0 || slotIndex >= equippedDrinks.Length || equippedDrinks[slotIndex] == null)
        {
            Debug.LogWarning($"Cannot remove ingredient from slot {slotIndex}: No drink equipped in this slot");
            return false;
        }

        bool removed = equippedDrinks[slotIndex].RemoveIngredient(ingredientIndex);
        if (removed)
        {
            Debug.Log($"Removed ingredient at index {ingredientIndex} from drink in slot {slotIndex}");
        }
        else
        {
            Debug.LogWarning($"Failed to remove ingredient at index {ingredientIndex} from drink in slot {slotIndex}");
        }

        return removed;
    }

    // Get all ingredients from a drink in a specific slot
    public List<Ingredient> GetIngredientsForDrink(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= equippedDrinks.Length || equippedDrinks[slotIndex] == null)
        {
            return new List<Ingredient>();
        }

        return equippedDrinks[slotIndex].GetIngredients();
    }

    // Get the max number of drinks that can be equipped
    public int GetMaxEquippedDrinks()
    {
        return maxEquippedDrinks;
    }
}