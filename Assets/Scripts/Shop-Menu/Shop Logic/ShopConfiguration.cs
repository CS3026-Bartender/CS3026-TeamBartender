using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopConfiguration : Object
{
    // public List<string> spiritIDs;
    public List<string> ingIDs;

    public ShopConfiguration(int numSps, int numIngs)
    {
        ingIDs = IngredientData.GetRandomList(numSps, numIngs);
    }

    public void RemoveIngredient(int slot)
    {
        ingIDs.RemoveAt(slot);
    }

    public string GetIngID(int slot)
    {
        if (slot >= ingIDs.Count)
        {
            return null;
        }
        else
        {
            return ingIDs[slot];
        }
    }

    public void DebugPrintConfig()
    {
        foreach (string ing in ingIDs) {
            IngredientData.DebugPrintIng(ing);
        }
    }
}
