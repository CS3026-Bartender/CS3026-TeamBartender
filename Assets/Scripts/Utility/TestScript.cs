using NUnit.Framework.Internal;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Sprite testSprite;

    void Start()
    {
        DataReader.Instance.LoadIngredients();
        // Temp ingredients for testing
        //IngredientData.AddIngredient("ing_raspberry", "Raspberry", 4.5f, "A tangy berry", testSprite);
        //IngredientData.AddSpirit("sp_rum", "Rum", 8f, "Yo ho ho", testSprite, 2, 25);
        //IngredientData.AddIngredient("ing_bitters", "Bitters", 2f, "Deepens the flavor", testSprite);
        //IngredientData.AddIngredient("ing_lemon", "Lemon", 3f, "A sour citrus", testSprite);

        ShopManager.Instance.InitializeShop();

        /*
        DrinkPurchaseManager.Instance.StartPurchase(0);
        DrinkPurchaseManager.Instance.CompletePurchase(0, 0);

        
        DrinkPurchaseManager.Instance.StartPurchase(1);
        DrinkPurchaseManager.Instance.CompletePurchase(0, 1);
        */
    }
}
