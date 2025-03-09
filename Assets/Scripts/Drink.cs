using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public struct Drink
{
    public string name;
    public int potency;
    public int richness;
    public int fruityness;
    public int bitterness;
    public float cooldownseconds;
    public Sprite drinkImage;
    public float cooldownRemaining;
    [HideInInspector] public Ingredient[] Ingredients;
}