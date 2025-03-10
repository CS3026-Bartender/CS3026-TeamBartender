using NUnit.Framework.Internal;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Sprite testSprite;
    [SerializeField] ShopManager shopManager;
    [SerializeField] DrinkMenuManager drinkMenuManager;

    void Start()
    {
        // Temp ingredients for testing
        IngredientData.AddIngredient("Raspberry", 4.5f, "A tangy berry", testSprite);
        IngredientData.AddSpirit("Rum", 8f, "Yo ho ho", testSprite);
        IngredientData.AddIngredient("Bitters", 2f, "Deepens the flavor", testSprite);
        IngredientData.AddIngredient("Lemon", 3f, "A sour citrus", testSprite);

        shopManager.InitializeShop();
        shopManager.BuyIngredient(1);
    }
}
