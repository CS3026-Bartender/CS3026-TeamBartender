using UnityEngine;

public class Ingredient
{
    public string DisplayName { get; private set; }
    public float Price { get; private set; }
    public string Description { get; private set; }
    public Sprite Icon { get; private set; }

    // Modifiers that ingredients apply to drinks
    public float ServeTimeModifier { get; private set; } = 0f;
    public float CustomerDrinkTimeModifier { get; private set; } = 0f;
    public float PotencyModifier { get; private set; } = 0f;

    public Ingredient(string name, float price, string desc, Sprite sprite,
                     float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f)
    {
        DisplayName = name;
        Price = price;
        Description = desc;
        Icon = sprite;
        ServeTimeModifier = serveTimeMod;
        CustomerDrinkTimeModifier = customerDrinkTimeMod;
        PotencyModifier = potencyMod;
    }

    public string GetDebug()
    {
        return DisplayName + ", $" + Price + ", " + Description;
    }
}