using UnityEngine;

public class TooltipIngredientDisplay : TooltipDisplay
{
    [SerializeField] protected IngredientDisplay ingDisplay;

    protected override void Display()
    {
        if (Tooltip.Instance == null)
        {
            if (DebugLogger.Instance.logTooltip) Debug.Log("Fail to display, no tooltip");
            return;
        }

        if (DebugLogger.Instance.logTooltip) Debug.Log("Displaying with TooltipIngredientDisplay method: " + ingDisplay.IngID);
        DrinkComponent comp = IngredientData.GetIngValue(ingDisplay.IngID);
        if (comp == null)
        {
            return;
        }
        header = comp.DisplayName;
        content = comp.Description;

        if (comp is Spirit sp)
        {
            content += "\nServe time: " + sp.BaseServeTime + " s";
            content += "\nDrink time: " + sp.BaseCustomerDrinkTime + " s";
            content += "\nPotency: " + sp.BasePotency;
            content += "\nDrink Price: " + sp.BaseDrinkPrice + " ¤";
        }
        else if (comp is Ingredient ing)
        {
            content += "\n";

            string end = "";

            if (ing.StatID == "serve_time")
            {
                content += "Serve time ";
                end = "s";
            }
            else if (ing.StatID == "drink_time")
            {
                content += "Drink time ";
                end = "s";
            }
            else if (ing.StatID == "potency")
            {
                content += "Potency ";
            }
            else if (ing.StatID == "drink_price")
            {
                content += "Drink Price ";
                end = "¤";
            }

            if (ing.IsMult)
            {
                content += "x" + ing.StatMod;
            }
            else
            {
                content += "+" + ing.StatMod + " " + end;
            }

        }

        Tooltip.Instance.Show(content, header);
    }
}
