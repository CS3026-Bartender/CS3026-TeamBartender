using System.Collections.Generic;
using UnityEngine;

public sealed class IngredientManager : Manager<IngredientManager>
{
    [SerializeField] private Sprite testSprite;

    private List<Ingredient> ingredients;

    private void Start()
    {
        // Temp ingredients for testing
        // TODO: replace with data read from csv
        Ingredient newIng = new Ingredient("Raspberry", 4.5f, "A tangy berry", testSprite);
    }
}
