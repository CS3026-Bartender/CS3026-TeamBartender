using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameDisplay;
    [SerializeField] private DraggableComponent dragComponent;
    [SerializeField] private string description;
    public string IngID { get; private set; }

    public void UpdateDisplay(string newID)
    {
        IngID = newID;
        Ingredient ing = IngredientData.GetIngValue(IngID);
        if (DebugLogger.Instance.logShopDisplay) Debug.Log("Updating ingredient " + nameDisplay.text + " to " + ing.DisplayName);
        icon.sprite = ing.Icon;
        nameDisplay.text = ing.DisplayName;
        description = ing.Description;
    }

    public void SetDraggable(bool canDrag)
    {
        dragComponent.CanDrag = canDrag;
    }
}
