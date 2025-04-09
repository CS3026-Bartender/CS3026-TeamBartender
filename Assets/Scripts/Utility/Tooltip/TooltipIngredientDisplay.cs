using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TooltipIngredientDisplay : TooltipDisplay
{
    
    [SerializeField] protected IngredientDisplay ingDisplay;

    public override void OnPointerEnter(PointerEventData eventData) 
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        header = ingDisplay.Name.text;
        content = ingDisplay.Desc;
        ToolTipManager.Show(content, header);
    }
}
