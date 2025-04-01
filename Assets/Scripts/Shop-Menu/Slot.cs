using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] protected IngredientDisplay ingDisplay;
    [SerializeField] protected DropArea dropArea;
    [SerializeField] protected GameObject ingDisplayPrefab;

    public virtual void UpdateSlot(string newID)
    {
        if (newID == null || IngredientData.GetIngValue(newID) == null)
        {
            SetEmpty();
            return;
        }
        if (ingDisplay == null)
        {
            GameObject newIngDisplayObj = Instantiate(ingDisplayPrefab, transform.position, transform.rotation, transform);
            ingDisplay = newIngDisplayObj.GetComponent<IngredientDisplay>();
        }
        ingDisplay.UpdateDisplay(newID);
    }

    public virtual void SetEmpty()
    {
        if (ingDisplay != null)
        {
            Destroy(ingDisplay.gameObject);
            ingDisplay = null;
        }
    }
}
