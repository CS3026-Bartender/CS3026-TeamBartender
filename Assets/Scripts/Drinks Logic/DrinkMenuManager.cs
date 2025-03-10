using UnityEngine;

public class DrinkMenuManager : Manager<DrinkMenuManager>
{
    [SerializeField] private DrinkData drinkData;

    // [SerializeField] int ingsPerDrinkAllowed = 3;
    [SerializeField] int drinksAllowed = 3;
    // need drink menu display

    public void AddIngredientToDrink(int drinkPos, string ing, int slot)
    {
        Drink drink = drinkData.GetDrink(drinkPos);
        if (drink == null)
        {
            if (IngredientData.GetIngValue(ing) is Spirit)
            {
                drink = drinkData.AddDrink("Drink Name", ing, drinkPos);
                Debug.Log(drink.drinkName + " added in manager");
            }
            else
            {
                Debug.Log("Can't add ingredient to drink " + drinkPos + "  without spirit");
                // TODO: tell UI failure, drink doesn't exist
                return;
            }
        }
        
        if (drink.GetIngID(slot) != null)
        {
            Debug.Log("Selling " + IngredientData.GetIngValue(drink.GetIngID(slot)));
            // TODO: tell currency system to add money
            // tell UI to display currency add
        }
        drink.AddIngredient(ing, slot);

        Debug.Log(IngredientData.GetIngValue(ing).DisplayName + " added to drink " + drinkPos + " in slot " + slot);

        // TODO: update UI
    }
}
