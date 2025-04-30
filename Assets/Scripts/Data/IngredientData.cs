using System.Collections.Generic;
using UnityEngine;

// TEMP IMPLEMENTATION WHILE WAITING FOR THERYN'S DESIGN

public static class IngredientData
{
    private static Dictionary<string, Ingredient> ingredients = new();
    private static Dictionary<string, Spirit> spirits = new();

    public static void AddIngredient(string id, string displayName, float price, float sellPrice, string desc, Sprite sprite,
                                float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f)
    {
        Ingredient newIng = new Ingredient(displayName, price, sellPrice, desc, sprite, serveTimeMod, customerDrinkTimeMod, potencyMod);
        bool success = ingredients.TryAdd(id, newIng);
        if (!success)
        {
            Debug.Log("Ingredient could not be added due to repeat ID");
        }
    }

    public static void AddSpirit(string id, string displayName, float price, float sellPrice, string desc, Sprite sprite,
                                float serveTime, float customerDrinkTime, float potency,
                                float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f)
    {
        List<IngredientMod> mods = new()
        {
            new IngredientMod(ModifierType.Additive, nameof(ServeTimeModifier), serveTimeMod),
            new IngredientMod(ModifierType.Additive, nameof(CustomerDrinkTimeModifier), customerDrinkTimeMod),
            new IngredientMod(ModifierType.Additive, nameof(PotencyModifier), potencyMod)
        };

        Spirit newSpirit = new Spirit(displayName, price, sellPrice, desc, sprite, serveTime, customerDrinkTime, potency,
                                     mods);
        bool success = spirits.TryAdd(id, newSpirit);
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
        if (id.StartsWith("sp"))
        {
            return spirits.GetValueOrDefault(id);
        }
        return ingredients.GetValueOrDefault(id);
    }

    public static List<string> GetRandomList(int numSps, int numIngs)
    {
        // get all ids
        List<string> sps = new(spirits.Keys);
        List<string> ings = new(ingredients.Keys);
        // put random ingredients in results list
        List<string> result = new();
        for (int i = 0; i < numSps; i++)
        {
            int index;
            // try/catch in case number of ingredients very small
            try
            {
                index = Random.Range(0, sps.Count); // random index in all
                result.Add(sps[index]); // add id to result
                sps.RemoveAt(index); // remove from all to avoid repeats
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return result;
            }
        }
        for (int i = 0; i < numIngs; i++)
        {
            int index;
            // try/catch in case number of ingredients very small
            try
            {
                index = Random.Range(0, ings.Count); // random index in all
                result.Add(ings[index]); // add id to result
                ings.RemoveAt(index); // remove from all to avoid repeats
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
