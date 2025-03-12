using UnityEngine;
using System.Collections.Generic;

public class DrinkMenuDisplay : MonoBehaviour
{
    public void RefreshDisplay()
    {
        // TODO: go through each drink/slot and update with data from DrinkData
        for (int i = 0; i < 3; i++)
        {
            Drink drink = DrinkData.Instance.GetDrink(i);
            if (drink == null)
            {

            }
        }
    }

    public void HighlightSlots(bool[][] data)
    {
        // TODO: go through each drink/slot and set available/unavailable based on data
    }
}
