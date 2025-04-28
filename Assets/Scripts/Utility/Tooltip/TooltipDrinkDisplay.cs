using UnityEngine;

public class TooltipDrinkDisplay : TooltipDisplay
{
    [SerializeField] protected Drink drink;

    protected override void Display()
    {
        if (Tooltip.Instance == null)
        {
            if (DebugLogger.Instance.logTooltip) Debug.Log("Fail to display, no tooltip");
            return;
        }

        int drinkPos = transform.GetSiblingIndex();
        if (DebugLogger.Instance.logTooltip) Debug.Log("Displaying with TooltipDrinkDisplay method: " + drinkPos);

        Drink drinkData = DrinkData.Instance.GetDrink(drinkPos);
        if (drinkData == null)
        {
            return;
        }

        header = drinkData.spiritID;

        if (drinkData.GetCalculatedSellPrice() != 0)
        {   // This one MUST remain an equal. Else it keeps appending drink data to the display until next round.
            content = "\nDrink Sell Price: " + drinkData.GetCalculatedSellPrice();
        }
        if (drinkData.GetCalculatedPotency() != 0)
        {
            content += "\nDrink Potency +" + drinkData.GetCalculatedPotency();
        }
        if (drinkData.GetCalculatedServeTime() != 0)
        {
            content += "\nDrink Serve Time: " + drinkData.GetCalculatedServeTime();
        }

        Tooltip.Instance.Show(content, header);
    }
}
