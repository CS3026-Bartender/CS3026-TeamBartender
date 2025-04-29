using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class Drink
{
    public string drinkName;
    public string spiritID;
    public string[] ingredients = new string[4];

    private Dictionary<(ModifierType, string), float> statTotals = new Dictionary<(ModifierType, string), float>();

    public Drink(string name, string spirit)
    {
        drinkName = name;
        spiritID = spirit;
        ingredients[0] = spirit;
        RecalcModifiers();
    }

    public void RecalcModifiers()
    {
        statTotals.Clear();

        foreach (var id in ingredients)
        {
            if (string.IsNullOrEmpty(id)) continue;
            var ing = IngredientData.GetIngValue(id);
            if (ing == null) continue;

            Accumulate(ModifierType.Additive, nameof(ing.ServeTimeModifier), ing.ServeTimeModifier);
            Accumulate(ModifierType.Additive, nameof(ing.CustomerDrinkTimeModifier), ing.CustomerDrinkTimeModifier);
            Accumulate(ModifierType.Additive, nameof(ing.PotencyModifier), ing.PotencyModifier);

            if (ing.Mods != null)
            {
                foreach (var m in ing.Mods)
                    Accumulate(m.ModifierType, m.statID, m.Value);
            }
        }
    }

    private void Accumulate(ModifierType type, string stat, float val)
    {
        var key = (type, stat);
        if(!statTotals.ContainsKey(key))
            statTotals[key] = (type == ModifierType.Multiplicative) ? 1f : 0f;
        statTotals[key] = (type == ModifierType.Multiplicative)
            ? statTotals[key] * val
            : statTotals[key] + val;
    }

    public float GetCalculatedServeTime()
    {
        if (ingredients[0] == null)
            return 0f;


        Ingredient baseIngredient = IngredientData.GetIngValue(ingredients[0]);
        if (!(baseIngredient is Spirit))
            return 0f;

        Spirit spirit = (Spirit)baseIngredient;
        float totalServeTime = ((Spirit)spirit).BaseServeTime;

        // Apply modifiers from all ingredients
        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                Ingredient ingredient = IngredientData.GetIngValue(ingredients[i]);
                totalServeTime += ingredient.ServeTimeModifier;
            }
        }

        return Mathf.Max(0.1f, totalServeTime); // Ensure serve time is at least 0.1 seconds
    }

    // Recalculate price based on all ingredients
    public float GetCalculatedPrice()
    {
        float totalPrice = 0f;

        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                Ingredient ingredient = IngredientData.GetIngValue(ingredients[i]);
                totalPrice += ingredient.Price;
            }
        }

        return totalPrice;
    }

    public float GetCalculatedSellPrice()
    {
        float totalSellPrice = 0f;

        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                Ingredient ingredient = IngredientData.GetIngValue(ingredients[i]);
                totalSellPrice += ingredient.SellPrice;
            }
        }

        return totalSellPrice;
    }

    // Get the customer drink time (how long it takes to drink)
    public float GetCalculatedCustomerDrinkTime()
    {
        if (ingredients[0] == null)
            return 0f;

        Ingredient baseIngredient = IngredientData.GetIngValue(ingredients[0]);
        if (!(baseIngredient is Spirit))
            return 0f;

        Spirit spirit = (Spirit)baseIngredient;
        float totalDrinkTime = spirit.BaseCustomerDrinkTime;

        // Apply modifiers from all ingredients
        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                Ingredient ingredient = IngredientData.GetIngValue(ingredients[i]);
                totalDrinkTime += ingredient.CustomerDrinkTimeModifier;
            }
        }

        return Mathf.Max(0.5f, totalDrinkTime); // Ensure drink time is at least 0.5 seconds
    }

    // Get the potency of the drink
    public float GetCalculatedPotency()
    {
        if (ingredients[0] == null)
            return 0f;

        Ingredient baseIngredient = IngredientData.GetIngValue(ingredients[0]);
        if (!(baseIngredient is Spirit))
            return 0f;

        Spirit spirit = (Spirit)baseIngredient;
        float totalPotency = spirit.BasePotency;

        // Apply modifiers from all ingredients
        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                Ingredient ingredient = IngredientData.GetIngValue(ingredients[i]);
                totalPotency += ingredient.PotencyModifier;
            }
        }

        return Mathf.Max(0f, totalPotency); // Ensure potency is not negative
    }

    public float GetStat(string statID)
    {
        var baseObj = IngredientData.GetIngValue(spiritID);
        if (!(baseObj is Spirit sp)) return 0f;

        var prop = typeof(Spirit).GetProperty("Base " + statID);
        if (prop == null) return 0f;
        float baseVal = (float)prop.GetValue(sp);

        statTotals.TryGetValue((ModifierType.Additive, statID + "Modifier"), out float add);
        statTotals.TryGetValue((ModifierType.Multiplicative, statID + "Modifier"), out float mult));

        return (baseVal + add) * (mult == 0f ? 1f : mult);
    }

    public void AddIngredient(string ing, int slot)
    {
        if (slot < 0 || slot >= ingredients.Length) return;
        ingredients[slot] = ing;

        if (slot == 0)
        {
            spiritID = ing;
        }
        RecalcModifiers();
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
