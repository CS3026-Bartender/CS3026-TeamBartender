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

    public Ingredient(string name, float price, float sellPrice, string desc, Sprite sprite, string tier,
                     float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f)
    {
        DisplayName = name;
        Price = price;
        SellPrice = price;
        Description = desc;
        Icon = sprite;
        Tier = tier;
        ServeTimeModifier = serveTimeMod;
        CustomerDrinkTimeModifier = customerDrinkTimeMod;
        PotencyModifier = potencyMod;
    }

    // Apply tier-based price scaling
    public float GetTieredPrice()
    {
        switch (Tier) 
        {
            case "Great":
                return Price * 1.2f;
            case "Epic":
                return Price * 1.5f
            default:
                return Price;
        }
    }

    public float GetTieredModifier(float baseValue, string modifierType)
    {
        switch (modifierType)
        {
            case "serveTime":
                return baseValue * (Tier == "Epic" ? 1.2f : 1f);
            case "potency":
                return baseValue * (Tier == "Great" ? 1.1f : 1f);
            default:
                return baseValue;
        }
    }

    public string GetDebug()
    {
        return $"{DisplayName} ({Tier}), ${Price}, {Description}";
    }
}
