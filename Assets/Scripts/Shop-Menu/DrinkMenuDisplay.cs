using UnityEngine;
using System.Collections.Generic;

public class DrinkMenuDisplay : MonoBehaviour
{
    public void RefreshDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            RefreshDisplay(i);
        }
    }

    public void RefreshDisplay(int drinkIndex)
    {
        Drink drink = DrinkData.Instance.GetDrink(drinkIndex);
        Transform drinkChild = transform.GetChild(drinkIndex);
        for (int j = 0; j < 4; j++)
        {
            GameObject slotObject = drinkChild.GetChild(j).gameObject;
            DrinkMenuSlot slot = slotObject.GetComponent<DrinkMenuSlot>();
            if (drink == null)
            {
                slot.SetEmpty();
            }
            else
            {
                slot.UpdateSlot(drink.GetIngID(j));
            }
        }
    }

    public void HighlightSlots(bool[][] data)
    {
        // TODO: go through each drink/slot and set available/unavailable based on data
    }
}
