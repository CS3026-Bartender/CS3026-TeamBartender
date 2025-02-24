using System.Collections.Generic;
using UnityEngine;

public sealed class IngredientManager : Manager<IngredientManager>
{
    [SerializeField] private Sprite testSprite;

    private List<Ingredient> ingredients = new();
    private List<Ingredient> spirits = new();

    public void InitializeIngredients()
    {
        // Temp ingredients for testing
        // TODO: replace with data read from csv
        AddIngredient("Raspberry", 4.5f, "A tangy berry", testSprite);
        AddSpirit("Rum", 8f, "Yo ho ho", testSprite);
        AddIngredient("Bitters", 2f, "Deepens the flavor", testSprite);
        AddIngredient("Lemon", 3f, "A sour citrus", testSprite);
    }

    public void AddIngredient(string name, float price, string desc, Sprite sprite)
    {
        Ingredient newIng = new(name, price, desc, sprite, false);
        ingredients.Add(newIng);
    }

    public void AddSpirit(string name, float price, string desc, Sprite sprite)
    {
        Ingredient newIng = new(name, price, desc, sprite, true);
        spirits.Add(newIng);
    }

    public List<Ingredient> GetRandomList(int numIngs)
    {
        List<Ingredient> all = new(spirits);
        all.AddRange(ingredients);
        List<Ingredient> result = new();
        for (int i = 0; i < numIngs; i++)
        {
            int index;
            try
            {
                index = Random.Range(0, all.Count);
                result.Add(all[index]);
                all.RemoveAt(index);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return result;
            }
        }
        return result;
    }

    public void DebugPrintIng(Ingredient ing)
    {
        Debug.Log(ing.GetDebug());
    }
}
