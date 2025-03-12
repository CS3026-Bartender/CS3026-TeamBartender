using TMPro;
using UnityEngine;

public class ShopSlot : Slot
{
    private void Start()
    {
        dropArea.IsDropAllowed = false;
    }

    private enum ShopState
    {
        CanBuy,
        CantBuy
    }

    private ShopState currentState;
    [SerializeField] private TextMeshProUGUI priceDisplay;

    public void SetCanBuy(bool canBuy)
    {
        currentState = canBuy ? ShopState.CanBuy : ShopState.CantBuy;
    }

    override public void UpdateSlot(string newID)
    {
        base.UpdateSlot(newID);
        priceDisplay.text = "$ " + IngredientData.GetIngValue(newID).Price;
    }
}