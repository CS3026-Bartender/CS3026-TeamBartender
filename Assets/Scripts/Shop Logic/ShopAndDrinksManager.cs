using UnityEngine;

public class ShopAndDrinksManager : Manager<ShopAndDrinksManager>
{
    private void Start()
    {
        IngredientManager.instance.InitializeIngredients();
        ShopManager.instance.InitializeShop();
    }
}
