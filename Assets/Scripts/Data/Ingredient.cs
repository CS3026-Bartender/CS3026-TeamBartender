using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ingredient : DrinkComponent
{
    public string StatID { get; set; }
    public float StatMod { get; set; }
    public bool IsMult { get; set; }

    public List<IngredientMod> Mods { get; private set; } 

    public Ingredient(string name, float price, float sellPrice, string desc, Sprite sprite,
                     string statID, float statMod, bool isMult) : base(name, price, sellPrice, desc, sprite)
    {
        StatID = statID;
        StatMod = statMod;
        IsMult = isMult;
        Mods = new List<IngredientMod>();
    }
}