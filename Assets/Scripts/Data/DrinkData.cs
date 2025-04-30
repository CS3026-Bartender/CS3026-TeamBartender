using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrinkData : MonoBehaviour
{
    public static DrinkData Instance { get; private set; }
    private readonly Dictionary<int, Drink> drinks = new(); // int is drink position

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetDrinks()
    {
        drinks.Clear();
        AddDrink("", "LiquorB1", 0);
    }

    /// <returns>Drink</returns>
    public Drink AddDrink(string name, string spirit, int drinkPos)
    {
        string newName = IngredientData.GetIngValue(spirit).DisplayName + " Cocktail";
        Drink drink = new(newName, spirit);
        drinks.Add(drinkPos, drink);
        Debug.Log(drinks[drinkPos].drinkName + " added");
        return drink;
    }

    public Drink GetDrink(int drinkPos)
    {
        Drink drink = drinks.GetValueOrDefault(drinkPos);
        return drink;
    }

    public List<Drink> GetAllDrinksAsList()
    {
        return drinks.Values.ToList();
    }

}
