using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEditor.Build;
using UnityEngine;

// TEMP IMPLEMENTATION WHILE WAITING FOR THERYN'S DESIGN

public static class IngredientData
{
    private static Dictionary<string, Ingredient> ingredients = new();

    public static void AddIngredient(string id, string displayName, float price, string desc, Sprite sprite,
                                float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f)
    {
        Ingredient newIng = new Ingredient(displayName, price, desc, sprite, serveTimeMod, customerDrinkTimeMod, potencyMod);
        bool success = ingredients.TryAdd(id, newIng);
        if (!success)
        {
            Debug.Log("Ingredient could not be added due to repeat ID");
        }
    }

    public static void AddSpirit(string id, string displayName, float price, string desc, Sprite sprite,
                                float serveTime, float customerDrinkTime, float potency,
                                float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f)
    {
        Spirit newSpirit = new Spirit(displayName, price, desc, sprite, serveTime, customerDrinkTime, potency,
                                     serveTimeMod, customerDrinkTimeMod, potencyMod);
        bool success = ingredients.TryAdd(id, newSpirit);
        if (!success)
        {
            Debug.Log("Spirit could not be added due to repeat ID");
        }
    }

    public static Ingredient GetIngValue(string id)
    {
        if (id == null)
        {
            return null;
        }
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
