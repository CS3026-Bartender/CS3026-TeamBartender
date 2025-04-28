using UnityEngine;

public class ResetGame : Manager<ResetGame>
{
    public void Reset()
    {
        if (DrinkData.Instance != null)
        {
            DrinkData.Instance.ResetDrinks();
        }
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.ResetMoney();
        }
    }
}
