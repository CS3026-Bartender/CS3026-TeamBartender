using UnityEngine;

public class DrinkMenuDisplay : MonoBehaviour
{
    [SerializeField] private GameObject drinkMenuObject;

    public void RefreshDisplay()
    {
        // TODO: go through each drink/slot and update with data from DrinkData
    }

    public void HighlightSlots(bool[][] data)
    {
        // TODO: go through each drink/slot and set available/unavailable based on data
    }
}
