using NUnit.Framework.Internal;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Sprite testSprite;
    [SerializeField] ShopManager shopMan;
    [SerializeField] DrinkMenuManager drinkMenuMan;
    [SerializeField] DrinkPurchaseManager drinkPurchaseMan;

    void Start()
    {

        // Temp ingredients for testing
        IngredientData.AddIngredient("ing_raspberry", "Raspberry", 4.5f, "A tangy berry", testSprite);
        IngredientData.AddSpirit("sp_rum", "Rum", 8f, "Yo ho ho", testSprite);
        IngredientData.AddIngredient("ing_bitters", "Bitters", 2f, "Deepens the flavor", testSprite);
        IngredientData.AddIngredient("ing_lemon", "Lemon", 3f, "A sour citrus", testSprite);

        shopMan.InitializeShop();
        drinkPurchaseMan.StartPurchase(0);
        drinkPurchaseMan.CompletePurchase(0, 0);

        drinkPurchaseMan.StartPurchase(1);
        drinkPurchaseMan.CompletePurchase(0, 1);
    }
}
