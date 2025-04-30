using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class Drink
{
    public string drinkName;
    public string spiritID;
    public string[] ingredients = new string[4];

    public Drink(string name, string spirit)
    {
        drinkName = name;
        spiritID = spirit;
        ingredients[0] = spirit;
    }

    public float GetCalculatedServeTime()
    {
        if (ingredients[0] == null)
            return 0f;


        DrinkComponent baseIngredient = IngredientData.GetIngValue(ingredients[0]);
        if (!(baseIngredient is Spirit))
            return 0f;

        Spirit spirit = (Spirit)baseIngredient;
        float totalServeTime = spirit.BaseServeTime;

        // Apply additive modifiers from all ingredients
        for (int i = 1; i < ingredients.Length; i++)
        {
            Ingredient ingredient = (Ingredient)IngredientData.GetIngValue(ingredients[i]);
            if (ingredient != null && !ingredient.IsMult && ingredient.StatID == "serve_time")
            {
                totalServeTime += ingredient.StatMod;
            }
        }
        // Apply multiplicative modifiers
        for (int i = 1; i < ingredients.Length; i++)
        {
            Ingredient ingredient = (Ingredient)IngredientData.GetIngValue(ingredients[i]);
            if (ingredient != null && ingredient.IsMult && ingredient.StatID == "serve_time")
            {
                totalServeTime *= ingredient.StatMod;
            }
        }

        return Mathf.Max(0.1f, totalServeTime); // Ensure serve time is at least 0.1 seconds
    }

    public float GetCalculatedSellPrice()
    {
        if (ingredients[0] == null)
            return 0f;


        DrinkComponent baseIngredient = IngredientData.GetIngValue(ingredients[0]);
        if (baseIngredient is not Spirit)
            return 0f;

        Spirit spirit = (Spirit)baseIngredient;

        float totalSellPrice = spirit.BaseDrinkPrice;

        // Apply additive modifiers from all ingredients
        for (int i = 1; i < ingredients.Length; i++)
        {
            Ingredient ingredient = (Ingredient)IngredientData.GetIngValue(ingredients[i]);
            if (ingredient != null && !ingredient.IsMult && ingredient.StatID == "drink_price")
            {
                totalSellPrice += ingredient.StatMod;
            }
        }
        // Apply multiplicative modifiers
        for (int i = 1; i < ingredients.Length; i++)
        {
            Ingredient ingredient = (Ingredient)IngredientData.GetIngValue(ingredients[i]);
            if (ingredient != null && ingredient.IsMult && ingredient.StatID == "drink_price")
            {
                totalSellPrice *= ingredient.StatMod;
            }
        }

        return totalSellPrice;
    }

    // Get the customer drink time (how long it takes to drink)
    public float GetCalculatedCustomerDrinkTime()
    {
        if (ingredients[0] == null)
            return 0f;

        DrinkComponent baseIngredient = IngredientData.GetIngValue(ingredients[0]);
        if (baseIngredient is not Spirit)
            return 0f;

        Spirit spirit = (Spirit)baseIngredient;
        float totalDrinkTime = spirit.BaseCustomerDrinkTime;

        // Apply additive modifiers from all ingredients
        for (int i = 1; i < ingredients.Length; i++)
        {
            Ingredient ingredient = (Ingredient)IngredientData.GetIngValue(ingredients[i]);
            if (ingredient != null && !ingredient.IsMult && ingredient.StatID == "drink_time")
            {
                totalDrinkTime += ingredient.StatMod;
            }
        }
        // Apply multiplicative modifiers
        for (int i = 1; i < ingredients.Length; i++)
        {
            Ingredient ingredient = (Ingredient)IngredientData.GetIngValue(ingredients[i]);
            if (ingredient != null && ingredient.IsMult && ingredient.StatID == "drink_time")
            {
                totalDrinkTime *= ingredient.StatMod;
            }
        }
        return Mathf.Min(0.5f, totalDrinkTime); // Ensure drink time is at least 0.5 seconds
    }

    // Get the potency of the drink
    public float GetCalculatedPotency()
    {
        if (ingredients[0] == null)
            return 0f;

        DrinkComponent baseIngredient = IngredientData.GetIngValue(ingredients[0]);
        if (baseIngredient is not Spirit)
            return 0f;

        Spirit spirit = (Spirit)baseIngredient;
        float totalPotency = spirit.BasePotency;

        // Apply additive modifiers from all ingredients
        for (int i = 1; i < ingredients.Length; i++)
        {
            Ingredient ingredient = (Ingredient)IngredientData.GetIngValue(ingredients[i]);
            if (ingredient != null && !ingredient.IsMult && ingredient.StatID == "potency")
            {
                totalPotency += ingredient.StatMod;
            }
        }
        // Apply multiplicative modifiers
        for (int i = 1; i < ingredients.Length; i++)
        {
            Ingredient ingredient = (Ingredient)IngredientData.GetIngValue(ingredients[i]);
            if (ingredient != null && ingredient.IsMult && ingredient.StatID == "potency")
            {
                totalPotency *= ingredient.StatMod;
            }
        }

        return Mathf.Max(0f, totalPotency); // Ensure potency is not negative
    }

    public void AddIngredient(string ing, int slot)
    {
        if (slot < 0 || slot >= ingredients.Length) return;
        ingredients[slot] = ing;

        if (slot == 0)
        {
            spiritID = ing;
        }
    }

    // slot is the index of the ingredient
    public string GetIngID(int slot)
    {
        return ingredients[slot];
    }

    public string GetSpiritID()
    {
        return spiritID;
    }
}
