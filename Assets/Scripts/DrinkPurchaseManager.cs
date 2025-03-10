using UnityEngine;

public class DrinkPurchaseManager : Manager<DrinkPurchaseManager>
{
    [SerializeField] private ShopManager shopMan;
    [SerializeField] private DrinkMenuManager drinkMan;
    [SerializeField] private DrinkData drinkData;
    // need display objects

    private bool purchaseActive = false;
    private int currentShopSlot;
    private string currentIng;

    private void FindValidSlots()
    {
        Ingredient ing = IngredientData.GetIngValue(currentIng);
        bool[][] slots = new bool[3][];

        // if spirit, first slot in each is available
        if (ing is Spirit)
        {
            bool[] spiritSlots = { true, false, false, false };
            System.Array.Fill(slots, spiritSlots);
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                slots[i] = new bool[4];
                slots[i][0] = false; // can't place non-spirit in first slot

                Drink d = drinkData.GetDrink(i);
                if (d != null)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        slots[i][j] = true;
                    }
                }
                else
                {
                    Debug.Log("No drink in position " + i);
                    System.Array.Fill(slots[i], false);
                }
            }
        }

        Debug.Log("Ingredient " + ing.DisplayName + " can go in slots:");
        foreach (bool[] arr in slots)
        {
            Debug.Log("[ " + string.Join(", ", arr) + " ]");
        }

        // tell drink menu display to highlight valid slots
    }

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

    public void CancelPurchase()
    {
        if (purchaseActive)
        {
            // TODO: tell UI to update
            EndPurchase();
        }
    }

    public void CompletePurchase(int drinkSlot, int drinkIngSlot)
    {
        if (purchaseActive)
        {
            Debug.Log("Attempting to place ingredient in drink " + drinkSlot + ", slot " + drinkIngSlot);

            // TODO: buy ingredient at shop slot, put in drink slot
            shopMan.BuyIngredient(currentShopSlot);
            drinkMan.AddIngredientToDrink(drinkSlot, currentIng, drinkIngSlot);

            EndPurchase();
        }
    }

    private void EndPurchase()
    {
        // TODO: update moving ingredient display

        purchaseActive = false;
    }
}
