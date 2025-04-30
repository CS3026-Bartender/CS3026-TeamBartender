

using UnityEngine;

public class DrinkComponent
{
    public string DisplayName { get; protected set; }
    public float Price { get; protected set; }

    public float SellPrice { get; protected set; }

    public string Description { get; protected set; }
    public Sprite Icon { get; protected set; }

    public DrinkComponent(string name, float price, float sellPrice, string desc, Sprite sprite)
    {
        DisplayName = name;
        Price = price;
        SellPrice = sellPrice;
        Description = desc;
        Icon = sprite;
    }

    public string GetDebug()
    {
        return $"{DisplayName}, ${Price}, {Description}";
    }
}
