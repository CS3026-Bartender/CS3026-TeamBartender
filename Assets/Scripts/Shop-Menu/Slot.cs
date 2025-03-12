using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] protected IngredientDisplay ingDisplay;
    [SerializeField] protected DropArea dropArea;

    public virtual void UpdateSlot(string newID)
    {
        ingDisplay.UpdateDisplay(newID);
    }
}
