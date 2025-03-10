using UnityEngine;

public class Drink
{
    public string drinkName;
    public string spiritID;
    public string[] ingredients = new string[4];

    public Drink(string name, string spirit)
    {
        drinkName = name;
        spiritID = spirit;
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

    public void AddIngredient(string ing, int slot)
    {
        ingredients[slot] = ing;
    }

    // slot is the index of the ingredient
    public string GetIngID(int slot)
    {
        return ingredients[slot];
    }
}
