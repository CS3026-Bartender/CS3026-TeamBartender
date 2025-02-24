using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopConfiguration : Object
{
    public List<Ingredient> ingredients;

    public ShopConfiguration(int numIngs)
    {
        ingredients = IngredientManager.instance.GetRandomList(numIngs);
        ingredients.Sort((Ingredient i1, Ingredient i2) =>
        {
            if (i2.isSpirit)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        });
    }

    public void DebugPrintConfig()
    {
        foreach (Ingredient ing in ingredients) {
            IngredientManager.instance.DebugPrintIng(ing);
        }
    }
}
