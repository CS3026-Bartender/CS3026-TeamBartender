using UnityEngine;
using System.Collections.Generic;

// This can be a standalone script file - no need to attach to a GameObject
public static class DrinkManager
{
    [System.Serializable]
    public struct Drink
    {
        public string name; // Changed from char[] to string for easier usage
        public int cost;
        public int potency;
        public int richness;
        public int fruityness;
        public int bitterness; // Fixed capitalization to match style
        public int cooldownseconds;
        public Sprite drinkImage;

    }

    private static List<Drink> allDrinks = new List<Drink>();

    // Initialize drinks (call this from a startup script)
    public static void InitializeDrinks()
    {
        // Load drinks from a ScriptableObject or other data source
        DrinkDatabase database = Resources.Load<DrinkDatabase>("DrinkDatabase");
        if (database != null)
        {
            allDrinks = new List<Drink>(database.drinks);
        }
    }

    // Get all drinks
    public static List<Drink> GetAllDrinks()
    {
        return allDrinks;
    }

    // Get drink by name
    public static Drink GetDrink(string drinkName)
    {
        return allDrinks.Find(d => d.name == drinkName);
    }
}

// Create a ScriptableObject to store your drinks data
[CreateAssetMenu(fileName = "DrinkDatabase", menuName = "Game/Drink Database")]
public class DrinkDatabase : ScriptableObject
{
    public List<DrinkManager.Drink> drinks = new List<DrinkManager.Drink>();
}