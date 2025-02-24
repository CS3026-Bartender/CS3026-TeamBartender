using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;

public class Ingredient : Object
{
    public string ingName;
    public float price;
    public string desc;
    public Sprite sprite;
    public bool isSpirit;

    public Ingredient(string name, float price, string desc, Sprite sprite, bool isSpirit)
    {
        // this.name = name;
        this.ingName = name;
        this.price = price;
        this.desc = desc;
        this.sprite = sprite;
        this.isSpirit = isSpirit;
    }

    public string GetDebug()
    {
        return ingName + ", $" + price + ", " + desc + ", is spirit: " + isSpirit;
    }
}
