using UnityEngine;

public class DrinkMenuManager : Manager<DrinkMenuManager>
{
    [SerializeField] int ingsPerDrinkAllowed = 3;
    [SerializeField] int drinksAllowed = 3;

    public void AddIngredientToDrink(int drinkPos, Ingredient ing, int slot)
    {
        if (drinkPos >= DrinkData.GetNumDrinks())
        {
            // TODO: tell UI failure, drink doesn't exist
            return;
        }
        Drink drink = DrinkData.GetDrink(drinkPos);
        
        if (drink.GetNumIngs() >= ingsPerDrinkAllowed)
        {
            drink.RemoveIngredient(slot);
            // TODO: tell currency system to add money
            // tell UI to display currency add
        }
        drink.AddIngredient(ing);

        // TODO: update UI
    }

    public void AddNewDrink(Ingredient spirit)
    {
        if (DrinkData.GetNumDrinks() >= drinksAllowed)
        {
            // TODO: tell UI failure, cannot add another drink
            return;
        }
        DrinkData.AddDrink("Drink Name", spirit);
        // TODO: update UI
    }
}
