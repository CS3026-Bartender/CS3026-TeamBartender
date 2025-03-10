using UnityEngine;

public class Ingredient : Object
{
    public string DisplayName { get; private set; }
    public float Price { get; private set; }
    public string Description { get; private set; }
    public Sprite Icon { get; private set; }

    public Ingredient(string name, float price, string desc, Sprite sprite)
    {
        // this.name = name;
        DisplayName = name;
        Price = price;
        Description = desc;
        Icon = sprite;
    }

    public string GetDebug()
    {
        return DisplayName + ", $" + Price + ", " + Description;
    }
}
