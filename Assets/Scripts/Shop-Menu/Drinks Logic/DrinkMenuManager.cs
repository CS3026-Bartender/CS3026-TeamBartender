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

        // Check if drink already has ingredient in slot
        string currIngID = drink.GetIngID(slot);
        if (currIngID != null)
        {
            // Sell old ingredient
            Ingredient currIng = IngredientData.GetIngValue(currIngID);
            Debug.Log("Selling " + currIng.DisplayName);

            // Spend currency
            // TODO: make currency always float or always int
            CurrencyManager.Instance.SpendMoney((int)currIng.Price);
        }

        // Add new ingredient
        drink.AddIngredient(ing, slot);

        Debug.Log(IngredientData.GetIngValue(ing).DisplayName + " added to drink " + drinkPos + " in slot " + slot);

        // Update UI
        drinkMenuDisplay.RefreshDisplay(drinkPos);
    }
}
