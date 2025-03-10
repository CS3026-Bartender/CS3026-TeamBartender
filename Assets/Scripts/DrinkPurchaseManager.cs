using UnityEngine;

public class DrinkPurchaseManager : Manager<DrinkPurchaseManager>
{
    [SerializeField] private ShopManager shopMan;
    [SerializeField] private DrinkMenuManager drinkMan;
    // Display objects

    private bool purchaseActive = false;
    private int currentShopSlot;

    private void FindValidSlots()
    {
        // TODO: tell drink menu display to highlight valid slots
    }

    public void StartPurchase(int shopSlot)
    {
        if (!purchaseActive)
        {
            purchaseActive = true;
            currentShopSlot = shopSlot;
            FindValidSlots();
        }
    }

    public void CancelPurchase()
    {
        if (purchaseActive)
        {
            // TODO: put ingredient back in shop slot

            EndPurchase();
        }
    }

    public void CompletePurchase(int drinkSlot, int drinkIngSlot)
    {
        if (purchaseActive)
        {
            // TODO: buy ingredient at shop slot, put in drink slot

            EndPurchase();
        }
    }

    private void EndPurchase()
    {
        // TODO: update moving ingredient display

        purchaseActive = false;
    }
}
