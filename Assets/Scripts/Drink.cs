using UnityEngine;

[System.Serializable]
public struct Drink
{
    public string name;
    public int potency;
    public int richness;
    public int fruityness;
    public int bitterness;
    public int cooldownseconds;
    public Sprite drinkImage;
    public float cooldownRemaining;
}