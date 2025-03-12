using UnityEngine;

public class DrinkMenuManager : Manager<DrinkMenuManager>
{
    [SerializeField] private DrinkData drinkData;

    // [SerializeField] int ingsPerDrinkAllowed = 3;
    // [SerializeField] int drinksAllowed = 3;
    [SerializeField] private DrinkMenuDisplay drinkMenuDisplay;

    public void AddIngredientToDrink(int drinkPos, string ing, int slot)
    {
        Drink drink = drinkData.GetDrink(drinkPos);
        // Check if drink exists
        if (drink == null)
        {
            // If spirit, make a new drink
            if (IngredientData.GetIngValue(ing) is Spirit)
            {
                drink = drinkData.AddDrink("Drink Name", ing, drinkPos);
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
        if (drink.GetIngID(slot) != null)
        {
            // Sell old ingredient
            Debug.Log("Selling " + IngredientData.GetIngValue(drink.GetIngID(slot)));
            // TODO: tell currency system to add money
            // tell UI to display currency add
        }

        // Add new ingredient
        drink.AddIngredient(ing, slot);

        Debug.Log(IngredientData.GetIngValue(ing).DisplayName + " added to drink " + drinkPos + " in slot " + slot);

        // TODO: update UI
    }
}
