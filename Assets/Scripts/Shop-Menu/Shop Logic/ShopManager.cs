using UnityEngine;

public sealed class ShopManager : Manager<ShopManager>
{
    [SerializeField] int ingsPerShopContents; // number of ingredients in the shop
    [SerializeField] DrinkMenuManager drinkMenuManager;
    [SerializeField] ShopDisplay shopDisplay;

    private ShopConfiguration currentShop;

    public void RefreshShop()
    {
        currentShop = new ShopConfiguration(ingsPerShopContents);
        shopDisplay.UpdateDisplay(currentShop);

        // TEMP: shop testing
        currentShop.DebugPrintConfig();
    }

    public void InitializeShop()
    {
        RefreshShop();
    }

    public string GetIngID(int slot)
    {
        return currentShop.GetIngID(slot);
    }

    public bool IsBuyAllowed(int slot)
    {
        string ingID = currentShop.GetIngID(slot);
        // TODO: check with currency system

        return true;
    }

    public void BuyIngredient(int slot)
    {
        string ingID = currentShop.GetIngID(slot);
        currentShop.RemoveIngredient(slot);

        // TODO: tell currency system to deduct price

        // TEMP: shop testing
        Debug.Log("Bought " + IngredientData.GetIngValue(ingID).DisplayName);
        currentShop.DebugPrintConfig();
    }
}
