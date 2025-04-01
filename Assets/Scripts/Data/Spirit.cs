using System.Collections.Generic;
using UnityEngine;

public class Spirit
{

    public float ServeTime { get; private set; }
    public float Potentcy { get; private set; }
    public float ServeTimeRemaining;

    // constructor = ingredient constructor
    public Spirit(string name, float price, string desc, Sprite sprite, float serveTime, float potentcy)
             : base(name, price, desc, sprite)
    {
        ServeTime = serveTime;
        Potentcy = potentcy;
    }







}

