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

    // Create a deep copy of this drink
    public Drink Clone()
    {
        Drink clone = new Drink
        {
            name = this.name,
            potency = this.potency,
            richness = this.richness,
            fruityness = this.fruityness,
            bitterness = this.bitterness,
            cooldownseconds = this.cooldownseconds,
            drinkImage = this.drinkImage,
            cooldownRemaining = this.cooldownRemaining
        };

        // Clone the ingredients array if it exists
        if (this.Ingredients != null)
        {
            clone.Ingredients = new Ingredient[this.Ingredients.Length];
            for (int i = 0; i < this.Ingredients.Length; i++)
            {
                clone.Ingredients[i] = this.Ingredients[i];
            }
        }

        return clone;
    }
}