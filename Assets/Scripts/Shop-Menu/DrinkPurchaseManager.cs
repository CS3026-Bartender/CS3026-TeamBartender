using UnityEngine;

public class DrinkPurchaseManager : Manager<DrinkPurchaseManager>
{
    [SerializeField] private ShopManager shopMan;
    [SerializeField] private DrinkMenuManager drinkMan;
    [SerializeField] private DrinkMenuDisplay drinkMenuDisplay;
    // [SerializeField] private DrinkData drinkData;
    // [SerializeField] private ShopDisplay shopDisplay;

    // purchase tracking
    private bool purchaseActive = false;
    private int currentShopSlot; // where did the ing come from?
    private string currentIng; // what ing is it?

    private void FindValidSlots()
    {
        DrinkComponent ing = IngredientData.GetIngValue(currentIng);
        bool[][] slots = new bool[3][];

        // if spirit, first slot in each is available
        if (ing is Spirit)
        {
            bool[] spiritSlots = { true, false, false, false };
            System.Array.Fill(slots, spiritSlots);
        }
        // otherwise the rest of slots are available
        else
        {
            for (int i = 0; i < 3; i++) // i is drink position
            {
                slots[i] = new bool[4];
                slots[i][0] = false; // can't place non-spirit in first slot

                Drink d = DrinkData.Instance.GetDrink(i);
                // if drink exists in this position, remaining slots are available
                if (d != null)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        slots[i][j] = true;
                    }
                }
                // otherwise all slots in drink are unavailable
                else
                {
                    Debug.Log("No drink in position " + i);
                    System.Array.Fill(slots[i], false);
                }
            }
        }

        // Testing: 
        //Debug.Log("Ingredient " + ing.DisplayName + " can go in slots:");
        //foreach (bool[] arr in slots)
        //{
        //    Debug.Log("[ " + string.Join(", ", arr) + " ]");
        //}

        // TODO: tell drink menu display to highlight valid slots
        drinkMenuDisplay.RefreshValid(slots);
    }

    // Call when drag ingredient starts
    public void StartPurchase(int shopSlot)
    {
        if (!purchaseActive)
        {
            purchaseActive = true;
            currentShopSlot = shopSlot;
            currentIng = shopMan.GetIngID(shopSlot);

            Debug.Log("Trying to buy the ingredient at slot " + shopSlot);

            FindValidSlots();
        }
    }

    // Call if ingredient dropped outside valid slot
    public void CancelPurchase()
    {
        if (purchaseActive)
        {
            EndPurchase();
        }
    }

    // Call if ingredient dropped on a valid slot
    public void CompletePurchase(int drinkSlot, int drinkIngSlot)
    {

        if (purchaseActive)
        {
            if (DebugLogger.Instance.logPurchase) Debug.Log("Attempting to place ingredient in drink " + drinkSlot + ", slot " + drinkIngSlot);

            if (ShopManager.Instance.IsBuyAllowed(currentShopSlot) && DrinkMenuManager.Instance.CheckAddOkay(drinkSlot, currentIng, drinkIngSlot))
            {
                if (DebugLogger.Instance.logPurchase) Debug.Log("Purchase is valid");
                // buy ingredient at shop slot, put in drink slot
                shopMan.BuyIngredient(currentShopSlot);
                drinkMan.AddIngredientToDrink(drinkSlot, currentIng, drinkIngSlot);
                EndPurchase();
            }
            else
            {
                if (DebugLogger.Instance.logPurchase) Debug.Log("Purchase is not valid, canceling");
                CancelPurchase();
            }
        }
        drinkMenuDisplay.FixDisplay();
        
    }

    private void EndPurchase()
    {
        // TODO: update moving ingredient display
        ShopManager.Instance.ReloadDisplay();
        purchaseActive = false;
    }
}
