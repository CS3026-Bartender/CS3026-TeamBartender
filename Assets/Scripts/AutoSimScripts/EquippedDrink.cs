using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EquippedDrink
{
    public string drinkName;
    public List<Ingredient> additionalIngredients = new List<Ingredient>();

    // Create an EquippedDrink from a base drink
    public EquippedDrink(string name)
    {
        drinkName = name;
    }

    // Add an ingredient to this equipped drink
    public void AddIngredient(Ingredient ingredient)
    {
        additionalIngredients.Add(ingredient);
    }

    // Remove an ingredient by index
    public bool RemoveIngredient(int index)
    {
        if (index >= 0 && index < additionalIngredients.Count)
        {
            additionalIngredients.RemoveAt(index);
            return true;
        }
        return false;
    }

    // Get all ingredients
    public List<Ingredient> GetIngredients()
    {
        return additionalIngredients;
    }

    // Calculate final stats for this drink with all additional ingredients
    public Drink GetFinalDrink(Drink baseDrink)
    {
        // Clone the base drink to avoid modifying the original
        Drink finalDrink = baseDrink.Clone();

        // Apply the effects of each additional ingredient
        foreach (Ingredient ingredient in additionalIngredients)
        {
            finalDrink.potency += Mathf.RoundToInt(ingredient.additionalPotentcy);
            finalDrink.fruityness += Mathf.RoundToInt(ingredient.additionalFruityness);
            finalDrink.bitterness += Mathf.RoundToInt(ingredient.additionalBitterness);
            finalDrink.richness += Mathf.RoundToInt(ingredient.additionalRichness);
        }

        // Set the ingredients array
        Ingredient[] allIngredients = new Ingredient[additionalIngredients.Count];
        for (int i = 0; i < additionalIngredients.Count; i++)
        {
            allIngredients[i] = additionalIngredients[i];
        }
        finalDrink.Ingredients = allIngredients;

        return finalDrink;
    }
}