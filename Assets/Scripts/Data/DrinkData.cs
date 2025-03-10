using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class DrinkData : MonoBehaviour
{
    private static List<Drink> drinks;

    public static void AddDrink(string name, Ingredient spirit)
    {
        Drink drink = new(name, spirit);
        drinks.Add(drink);
    }

    public static void ForEachDrink(UnityAction<Drink> action)
    {
        foreach (Drink d in drinks)
        {
            action.Invoke(d);
        }
    }

    public static Drink GetDrink(int drinkPos)
    {
        return drinks[drinkPos];
    }

    public static int GetNumDrinks()
    {
        return drinks.Count;
    }
}
