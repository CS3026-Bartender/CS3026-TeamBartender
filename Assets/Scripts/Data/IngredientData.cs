using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEditor.Build;
using UnityEngine;

// TEMP IMPLEMENTATION WHILE WAITING FOR THERYN'S DESIGN

public static class IngredientData
{
    private static Dictionary<string, Ingredient> ingredients = new();

    public static void AddIngredient(string id, string displayName, float price, string desc, Sprite sprite)
    {
        Ingredient newIng = new(displayName, price, desc, sprite);
        bool success = ingredients.TryAdd(id, newIng);
        if (!success)
        {
            Debug.Log("Ingredient could not be added due to repeat ID");
        }
    }

    public static void AddSpirit(string id, string displayName, float price, string desc, Sprite sprite)
    {
        Spirit newSpirit = new(displayName, price, desc, sprite);
        bool success = ingredients.TryAdd(id, newSpirit);
        if (!success)
        {
            Debug.Log("Spirit could not be added due to repeat ID");
        }
    }

    public static Ingredient GetIngValue(string id)
    {
        return ingredients.GetValueOrDefault(id);
    }

    public static List<string> GetRandomList(int numIngs)
    {
        List<string> all = new(ingredients.Keys);
        List<string> result = new();
        for (int i = 0; i < numIngs; i++)
        {
            int index;
            try
            {
                index = Random.Range(0, all.Count);
                Debug.Log("Random ingredient num: " + index);
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

    public static void DebugPrintIng(string ingID)
    {
        Ingredient ing = ingredients.GetValueOrDefault(ingID);
        Debug.Log(ing != null ? ing.GetDebug() : "Ingredient does not exist");
    }
}
