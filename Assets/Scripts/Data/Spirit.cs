using System.Collections.Generic;
using UnityEngine;

public class Spirit : Ingredient
{
    public Spirit(string name, float price, string desc, Sprite sprite) : base(name, price, desc, sprite) { }

    private Dictionary<string, float> stats = new(); // stat id, stat value

    public float GetStat(string id)
    {
        return stats.GetValueOrDefault(id);
    }
}

