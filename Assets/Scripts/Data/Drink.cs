using System.Collections.Generic;
using UnityEngine;

public class Drink : Object
{
    public string drinkName;
    public Ingredient spirit;
    public List<Ingredient> ingredients;

    public Drink(string name, Ingredient spirit)
    {
        drinkName = name;
        this.spirit = spirit;
    }

    public float GetCalculatedServeTime()
    {
        // TODO
        return 0f;
    }

    public float GetCalculatedPrice()
    {
        // TODO
        return 0f;
    }

    public int GetNumIngs()
    {
        return ingredients.Count;
    }

    public void AddIngredient(Ingredient ing)
    {
        ingredients.Add(ing);
    }

    public void RemoveIngredient(int slot)
    {
        ingredients.RemoveAt(slot);
    }
}
