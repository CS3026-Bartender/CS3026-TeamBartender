using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameDisplay;
    [SerializeField] private DraggableComponent dragComponent;
    private string ingID;

    public void UpdateDisplay(string newID)
    {
        ingID = newID;
        Ingredient ing = IngredientData.GetIngValue(ingID);
        icon.sprite = ing.Icon;
        nameDisplay.text = ing.DisplayName;
    }

    public void SetDraggable(bool canDrag)
    {
        dragComponent.CanDrag = canDrag;
    }
}
