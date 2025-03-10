using System.Collections.Generic;
using UnityEngine;

public class Spirit : Ingredient
{
    public Spirit(string name, float price, string desc, Sprite sprite, float simPrice, float serveTime)
        : base(name, price, desc, sprite)
    {
        SimPrice = simPrice;
        ServeTime = serveTime;
    }

    public float SimPrice { get; private set; }
    public float ServeTime { get; private set; }
}

