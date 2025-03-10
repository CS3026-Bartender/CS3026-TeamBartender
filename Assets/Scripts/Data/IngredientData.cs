using System.Collections.Generic;
using UnityEngine;

// TEMP IMPLEMENTATION WHILE WAITING FOR THERYN'S DESIGN

public static class IngredientData
{
    private static List<Ingredient> ingredients = new();
    private static List<Ingredient> spirits = new();

    public static void AddIngredient(string name, float price, string desc, Sprite sprite)
    {
        Ingredient newIng = new(name, price, desc, sprite, false);
        ingredients.Add(newIng);
    }

    public static void AddSpirit(string name, float price, string desc, Sprite sprite)
    {
        Ingredient newIng = new(name, price, desc, sprite, true);
        spirits.Add(newIng);
    }

    public static List<Ingredient> GetRandomList(int numIngs)
    {
        List<Ingredient> all = new(spirits);
        all.AddRange(ingredients);
        List<Ingredient> result = new();
        for (int i = 0; i < numIngs; i++)
        {
            int index;
            try
            {
                index = Random.Range(0, all.Count);
                result.Add(all[index]);
                all.RemoveAt(index);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return result;
            }
        }
        return result;
    }

    public static void DebugPrintIng(Ingredient ing)
    {
        Debug.Log(ing.GetDebug());
    }
}
