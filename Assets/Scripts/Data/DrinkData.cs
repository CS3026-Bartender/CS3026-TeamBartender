using System.Collections.Generic;
using UnityEngine;

public class DrinkData : MonoBehaviour
{
    private readonly Dictionary<int, Drink> drinks = new(); // int drinkPos

    public Drink AddDrink(string name, string spirit, int drinkPos)
    {
        Drink drink = new(name, spirit);
        drinks.Add(drinkPos, drink);
        Debug.Log(drinks[drinkPos].drinkName + " added");
        return drink;
    }

    public Drink GetDrink(int drinkPos)
    {
        Drink drink = drinks.GetValueOrDefault(drinkPos);
        return drink;
    }
}
