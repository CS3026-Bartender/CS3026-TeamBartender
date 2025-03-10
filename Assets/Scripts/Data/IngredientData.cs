using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TEMP IMPLEMENTATION WHILE WAITING FOR THERYN'S DESIGN

public static class IngredientData
{
    private static Dictionary<string, Ingredient> ingredients = new();

    public static void AddIngredient(string id, string displayName, float price, string desc, Sprite sprite)
    {
        Ingredient newIng = new(displayName, price, desc, sprite);
        ingredients.Add(id, newIng);
    }

    public static void AddSpirit(string id, string displayName, float price, string desc, Sprite sprite)
    {
        Spirit newSpirit = new(displayName, price, desc, sprite);
        ingredients.Add(id, newSpirit);
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
        Debug.Log(ing == null ? ing.GetDebug() : "Ingredient does not exist");
    }
}
