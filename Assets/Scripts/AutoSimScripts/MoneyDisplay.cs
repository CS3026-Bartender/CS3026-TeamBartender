using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private string prefix = "$";

    private void OnEnable()
    {
        // Subscribe to money changed event
        CurrencyManager.OnMoneyChanged += UpdateMoneyDisplay;

        // Initial update
        if (CurrencyManager.Instance != null)
        {
            UpdateMoneyDisplay(CurrencyManager.Instance.Money);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from event when disabled
        CurrencyManager.OnMoneyChanged -= UpdateMoneyDisplay;
    }

    // Update the UI text with the current money value
    private void UpdateMoneyDisplay(int currentMoney)
    {
        if (moneyText != null)
        {
            moneyText.text = $"{prefix}{currentMoney}";

          
        }
    }

 
}