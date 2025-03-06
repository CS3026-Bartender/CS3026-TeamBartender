using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public sealed class ShopManager : Manager<ShopManager>
{
    [SerializeField] int ingsPerShopContents;
    [SerializeField] Sprite testSprite;

    private ShopConfiguration currentShop;
    // private ShopDisplay shopDisplay;

    private void Start()
    {
        InitializeShop();
    }

    public void RefreshShop()
    {
        currentShop = new ShopConfiguration(ingsPerShopContents);
        // shopDisplay.UpdateDisplay();
    }

    public void InitializeShop()
    {
        // Temp ingredients for testing
        // TODO: replace with data read from csv
        IngredientData.AddIngredient("Raspberry", 4.5f, "A tangy berry", testSprite);
        IngredientData.AddSpirit("Rum", 8f, "Yo ho ho", testSprite);
        IngredientData.AddIngredient("Bitters", 2f, "Deepens the flavor", testSprite);
        IngredientData.AddIngredient("Lemon", 3f, "A sour citrus", testSprite);

        RefreshShop();

        // TEMP: shop testing
        currentShop.DebugPrintConfig();

        RefreshShop();
        currentShop.DebugPrintConfig();
    }
}
