using UnityEngine;

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
            HighlightUI highlight = slotObject.GetComponent<HighlightUI>();
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

    public void RefreshValid(bool[][] slots) 
    {
        for (int i=0; i<3; i++)
        {
            Drink drink = DrinkData.Instance.GetDrink(i);
            Transform drinkChild = transform.GetChild(i);
            for (int j=0; j<4; j++) 
            {
                GameObject slotObject = drinkChild.GetChild(j).gameObject;
                DrinkMenuSlot slot = slotObject.GetComponent<DrinkMenuSlot>();
                HighlightUI highlight = slotObject.GetComponent<HighlightUI>();
                if (slots[i][j])
                {
                    highlight.HighlightValid();
                }
                else {
                    highlight.HighlightInvalid();
                }
            }
        }
    }

    public void FixDisplay() {
        for (int i=0; i<3; i++)
        {
            Drink drink = DrinkData.Instance.GetDrink(i);
            Transform drinkChild = transform.GetChild(i);
            for (int j=0; j<4; j++)
            {
                GameObject slotObject = drinkChild.GetChild(j).gameObject;
                DrinkMenuSlot slot = slotObject.GetComponent<DrinkMenuSlot>();
                HighlightUI highlight = slotObject.GetComponent<HighlightUI>();

                highlight.DehighlightImage();
            }
        }
    }

}
