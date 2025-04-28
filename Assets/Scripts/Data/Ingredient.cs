using System.Collections.Generic;
using UnityEngine;

public class Ingredient
{
    public string DisplayName { get; private set; }
    public float Price { get; private set; }

    public float SellPrice { get; private set; }

    public string Description { get; private set; }
    public Sprite Icon { get; private set; }

    // Modifiers that ingredients apply to drinks
    public float ServeTimeModifier { get; private set; } = 0f;
    public float CustomerDrinkTimeModifier { get; private set; } = 0f;
    public float PotencyModifier { get; private set; } = 0f;

    // Track tiers
    public string Tier { get; private set; }
    public List<IngredientMod> Mods { get; private set; }

    public Ingredient(string name, float price, float sellPrice, string desc, Sprite sprite,
                     float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f,
                     string tier = "Normal")
    {
        DisplayName = name;
        Price = price;
        SellPrice = price;
        Description = desc;
        Icon = sprite;
        ServeTimeModifier = serveTimeMod;
        CustomerDrinkTimeModifier = customerDrinkTimeMod;
        PotencyModifier = potencyMod;
        Tier = tier;

        Mods = new List<IngredientMod>
        {
            new IngredientMod(ModifierType.Add, nameof(Spirit.BaseServeTime), serveTimeMod),
            new IngredientMod(ModifierType.Add, nameof(Spirit.BaseCustomerDrinkTime), customerDrinkTimeMod),
            new IngredientMod(ModifierType.Add, nameof(Spirit.BasePotency), potencyMod),

            new IngredientMod(ModifierType.Multiply, nameof(Spirit.BasePotency), tier == "Great" ? 1.10f : 1f),
            new IngredientMod(ModifierType.Multiply, nameof(Spirit.BasePotency), tier == "Epic" ? 1.25f : 1f)
        };
    }

    public string GetDebug()
    {
        return $"{DisplayName} ({Tier}), ${Price}/{SellPrice}, {Description}";
    }
}
