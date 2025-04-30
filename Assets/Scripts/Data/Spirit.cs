using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spirit : DrinkComponent
{
    public float BaseServeTime { get; private set; }
    public float BaseCustomerDrinkTime { get; private set; }
    public float BasePotency { get; private set; }
    public float BaseDrinkPrice { get; private set; }

    public Dictionary<string, float> BaseStats { get; private set; }
    // constructor = ingredient constructor
    public Spirit(string name, float price, float sellPrice, string desc, Sprite sprite,
                 float serveTime, float customerDrinkTime, float potency, float drinkPrice) 
                : base(name, price, sellPrice, desc, sprite)
    {
        BaseServeTime = serveTime;
        BaseCustomerDrinkTime = customerDrinkTime;
        BasePotency = potency;
        BaseDrinkPrice = drinkPrice;

        BaseStats = new Dictionary<string, float>
        {
            { nameof(BaseServeTime), serveTime },
            { nameof(BaseCustomerDrinkTime), customerDrinkTime },
            { nameof(BasePotency), potency },
            { nameof(BaseDrinkPrice), drinkPrice }
        };
    }
}
