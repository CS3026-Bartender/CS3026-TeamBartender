using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameDisplay;
    private string ingID;

    public void UpdateDisplay(string newID)
    {
        // TODO: update display with new ingredient details
    }
}
