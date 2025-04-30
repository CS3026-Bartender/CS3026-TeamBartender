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

        header = drinkData.drinkName;

        content = "Serve Time: " + drinkData.GetCalculatedServeTime() + "s";
        content += "\nDrink Time: " + drinkData.GetCalculatedCustomerDrinkTime() + "s";
        content += "\nPotency +" + drinkData.GetCalculatedPotency();
        content += "\nDrink Price: " + drinkData.GetCalculatedSellPrice() + "¤";

        Tooltip.Instance.Show(content, header);
    }
}
