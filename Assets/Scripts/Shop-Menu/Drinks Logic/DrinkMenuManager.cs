using UnityEngine;

public class DrinkMenuManager : Manager<DrinkMenuManager>
{
    // [SerializeField] private DrinkData drinkData;

    // [SerializeField] int ingsPerDrinkAllowed = 3;
    // [SerializeField] int drinksAllowed = 3;
    [SerializeField] private DrinkMenuDisplay drinkMenuDisplay;

    private void Start()
    {
        drinkMenuDisplay.RefreshDisplay();
    }

    public bool CheckAddOkay(int drinkPos, string ing, int slot)
    {
        Drink drink = DrinkData.Instance.GetDrink(drinkPos);

        if (IngredientData.GetIngValue(ing) is Spirit)
        {
            // spirit has to be in slot 0
            if (slot != 0)
            {
                return false;
            }
        }
        else
        {
            // non-spirit can't be in 0 and has to already have drinkE
            if (slot == 0 || drink == null)
            {
                return false;
            }
        }
        return true;
    }

    public void AddIngredientToDrink(int drinkPos, string ing, int slot)
    {
        Drink drink = DrinkData.Instance.GetDrink(drinkPos);

        // Check if drink exists
        if (drink == null)
        {
            // If spirit, make a new drink
            if (IngredientData.GetIngValue(ing) is Spirit)
            {
                drink = DrinkData.Instance.AddDrink("Drink Name", ing, drinkPos);
                Debug.Log(drink.drinkName + " added in manager");
            }
            // Otherwise, can't add ingredient
            else
            {
                Debug.Log("Can't add ingredient to drink " + drinkPos + "  without spirit");
                // TODO: tell UI failure, drink doesn't exist
                return;
            }
        }
        else
        {
            // Check if drink already has ingredient in slot
            string currIngID = drink.GetIngID(slot);
            if (currIngID != null)
            {
                // Sell old ingredient
                DrinkComponent currIng = IngredientData.GetIngValue(currIngID);
                if (DebugLogger.Instance.logDrinkLogic) Debug.Log("Selling " + currIng.DisplayName);

                // Get currency
                // TODO: make currency always float or always int
                CurrencyManager.Instance.AddMoney((int)currIng.SellPrice);
            }
        }

        // Add new ingredient
        drink.AddIngredient(ing, slot);

        if (DebugLogger.Instance.logDrinkLogic) Debug.Log(IngredientData.GetIngValue(ing).DisplayName + " added to drink " + drinkPos + " in slot " + slot);

        // Update UI
        drinkMenuDisplay.RefreshDisplay(drinkPos);
    }
}
