using System.Collections.Generic;
using UnityEngine;
public class Spirit : Ingredient
{
    public float BaseServeTime { get; private set; }
    public float BaseCustomerDrinkTime { get; private set; }
    public float BasePotency { get; private set; }

    public Dictionary<string, float> BaseStats { get; private set; }
    // constructor = ingredient constructor
    public Spirit(string name, float price, float sellPrice, string desc, Sprite sprite,
                 float serveTime, float customerDrinkTime, float potency,
                 List<IngredientMod>  mods = null) 
                : base(name, price, sellPrice, desc, sprite,
                
                    mods?.FindAll(m => m.StatID == nameof(ServeTimeModifier) && mods.modifierType = ModifierType.Additive)
                        .Sum(m => m.Value) ?? 0f,
                    mods?.FindAll(m => m.statID == nameof(CustomerDrinkTimeModifier) && m.ModifierType == ModifierType.Additive)
                        .Sum(m => m.Value) ?? 0f,
                    mods?.FindAll(m => m.statID == nameof(PotencyModifier) && m.ModifierType == ModifierType.Additive)
                        .Sum(m => m.Value) ?? 0f,
                    )
    {
        BaseServeTime = serveTime;
        BaseCustomerDrinkTime = customerDrinkTime;
        BasePotency = potency;
        if (mods != null) mods = mods;
    }
}
