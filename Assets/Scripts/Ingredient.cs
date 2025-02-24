using UnityEngine;

public class Ingredient : Object
{
    public float price;
    public string desc;
    public Sprite sprite;

    public Ingredient(string name, float price, string desc, Sprite sprite)
    {
        this.name = name;
        this.price = price;
        this.desc = desc;
        this.sprite = sprite;
    }
}
