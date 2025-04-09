using System.Collections.Generic;
using UnityEngine;

public class Spirit : Ingredient
{
    public float BaseServeTime { get; private set; }
    public float BaseCustomerDrinkTime { get; private set; }
    public float BasePotency { get; private set; }

    // constructor = ingredient constructor
    public Spirit(string name, float price, string desc, Sprite sprite,
                 float serveTime, float customerDrinkTime, float potency,
                 float serveTimeMod = 0f, float customerDrinkTimeMod = 0f, float potencyMod = 0f)
             : base(name, price, desc, sprite, serveTimeMod, customerDrinkTimeMod, potencyMod)
    {
        BaseServeTime = serveTime;
        BaseCustomerDrinkTime = customerDrinkTime;
        BasePotency = potency;
    }
}
