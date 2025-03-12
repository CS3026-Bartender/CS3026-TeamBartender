using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopConfiguration : Object
{
    public List<string> spiritIDs;
    public List<string> ingIDs;

    public ShopConfiguration(int numIngs)
    {
        ingIDs = IngredientData.GetRandomList(numIngs);
        ingIDs.Sort((string i1, string i2) =>
        {
            if (IngredientData.GetIngValue(i1) is Spirit)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        });
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
