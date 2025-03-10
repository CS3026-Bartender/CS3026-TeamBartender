using UnityEngine;

public sealed class ShopManager : Manager<ShopManager>
{
    [SerializeField] int ingsPerShopContents;
    [SerializeField] DrinkMenuManager drinkMenuManager;

    private ShopConfiguration currentShop;
    // private ShopDisplay shopDisplay;

    public void RefreshShop()
    {
        currentShop = new ShopConfiguration(ingsPerShopContents);
        // shopDisplay.UpdateDisplay(currentShop);

        // TEMP: shop testing
        currentShop.DebugPrintConfig();
    }

    public void InitializeShop()
    {
        RefreshShop();
    }

    public Ingredient GetIngredient(int slot)
    {
        return currentShop.GetIngredient(slot);
    }

    public void BuyIngredient(int slot)
    {
        // TODO: check with currency system that buy is allowed

        Ingredient ing = currentShop.GetIngredient(slot);
        currentShop.RemoveIngredient(slot);

        // TODO: tell currency system to deduct price
        // tell UI to remove from shop display

        if (ing.isSpirit)
        {
            
        }

        // TEMP: shop testing
        currentShop.DebugPrintConfig();
    }
}
