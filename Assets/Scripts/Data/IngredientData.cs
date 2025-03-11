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
        // Check if add worked
        if (!success)
        {
            Debug.Log("Ingredient could not be added due to repeat ID");
        }
    }

    public static void AddSpirit(string id, string displayName, float shopPrice, string desc, Sprite sprite, float simPrice, float serveTime)
    {
        Spirit newSpirit = new(displayName, shopPrice, desc, sprite, simPrice, serveTime);
        bool success = ingredients.TryAdd(id, newSpirit);
        // Check if add worked
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
        // get all ids
        List<string> all = new(ingredients.Keys);
        // put random ingredients in results list
        List<string> result = new();
        for (int i = 0; i < numIngs; i++)
        {
            int index;
            // try/catch in case number of ingredients very small
            try
            {
                index = Random.Range(0, all.Count); // random index in all
                result.Add(all[index]); // add id to result
                all.RemoveAt(index); // remove from all to avoid repeats
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
