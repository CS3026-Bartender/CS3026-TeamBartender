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
        Ingredient ing = IngredientData.GetIngValue(ingDisplay.IngID);
        if (ing == null)
        {
            return;
        }
        header = ing.DisplayName;
        content = ing.Description;

        if (ing is Spirit)
        {
            Spirit sp = (Spirit)ing;
            content += "\nServe time: " + sp.BaseServeTime;
            content += "\nDrink time: " + sp.BaseCustomerDrinkTime;
            content += "\nPotency: " + sp.BasePotency;
            content += "\nPrice: " + sp.Price;
        }
        else
        {
            if (ing.ServeTimeModifier != 0)
            {
                content += "\nServe time +" + ing.ServeTimeModifier;
            }
            if (ing.CustomerDrinkTimeModifier != 0)
            {
                content += "\nDrink time +" + ing.CustomerDrinkTimeModifier;
            }
            if (ing.PotencyModifier != 0)
            {
                content += "\nPotency +" + ing.PotencyModifier;
            }
            if (ing.Price != 0)
            {
                content += "\nPrice +" + ing.Price;
            }
        }

        Tooltip.Instance.Show(content, header);
    }
}
