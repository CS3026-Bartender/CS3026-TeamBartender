using System.Collections.Generic;
using UnityEngine;

public class Spirit : Ingredient
{

    public float ServeTime { get; private set; }
    public float Potency { get; private set; }
    public float ServeTimeRemaining;

    // constructor = ingredient constructor
    public Spirit(string name, float price, string desc, Sprite sprite, float serveTime, float potency)
             : base(name, price, desc, sprite)
    {
        ServeTime = serveTime;
        Potency = potency;
    }







}

