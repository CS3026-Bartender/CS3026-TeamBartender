using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TooltipDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string content;
    public string header;

    public virtual void OnPointerEnter(PointerEventData eventData) 
    {
        ToolTipManager.Show(content, header);
    }

    public virtual void OnPointerExit(PointerEventData eventData) 
    {
        ToolTipManager.Hide();
    }
}
