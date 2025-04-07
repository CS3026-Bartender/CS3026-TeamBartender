using UnityEngine;

public sealed class ShopManager : Manager<ShopManager>
{
    [SerializeField] int ingsPerShopContents; // number of ingredients in the shop
    // [SerializeField] DrinkMenuManager drinkMenuManager;
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
        Ingredient ing = IngredientData.GetIngValue(ingID);

        // check ing exists
        if (ing == null)
        {
            return false;
        }

        // check with currency system
        if (CurrencyManager.Instance.Money < ing.Price)
        {
            return false;
        }

        return true;
    }

    public void BuyIngredient(int slot)
    {
        string ingID = currentShop.GetIngID(slot);
        currentShop.RemoveIngredient(slot);
        shopDisplay.UpdateDisplay(currentShop);

        // tell currency system to deduct price
        CurrencyManager.Instance.SpendMoney((int)IngredientData.GetIngValue(ingID).Price);


        if (DebugLogger.Instance.logShopLogic)
        {
            Debug.Log("Bought " + IngredientData.GetIngValue(ingID).DisplayName);
            currentShop.DebugPrintConfig();
        }
    }

    public void ReloadDisplay()
    {
        shopDisplay.UpdateDisplay(currentShop);
    }
}
