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
    public float ServeTimeModifier { get; private set; }
    public float CustomerDrinkTimeModifier { get; private set; }
    public float PotencyModifier { get; private set; }

    public List<IngredientMod> Mods { get; private set: } 

    public Ingredient(string name, float price, float sellPrice, string desc, Sprite sprite,
                     float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f)
    {
        DisplayName = name;
        Price = price;
        SellPrice = price;
        Description = desc;
        Icon = sprite;
        ServeTimeModifier = serveTimeMod;
        CustomerDrinkTimeModifier = customerDrinkTimeMod;
        PotencyModifier = potencyMod;
        Mods = new List<IngredientMod();
    }

    public Ingredient(string name, float price, float sellPrice, string desc, Sprite sprite,
                     List<IngredientMod> mods
    ) : this(name, price, sellPrice, desc, sprite,
        mods?.FindAll(m => m.StatID == nameof(ServeTimeModifier) && mods.ModifierType = ModifierType.Additive)
            .Sum(m => m.Value),
        mods?.FindAll(m => m.statID == nameof(CustomerDrinkTimeModifier) && m.ModifierType == ModifierType.Additive)
            .Sum(m => m.Value),
        mods?.FindAll(m => m.statID == nameof(PotencyModifier) && m.ModifierType == ModifierType.Additive)
            .Sum(m => m.Value))
    {
        Mods = mods;
    }

    public string GetDebug()
    {
        return $"{DisplayName} ({Tier}), ${Price}, {Description}";
    }
}
