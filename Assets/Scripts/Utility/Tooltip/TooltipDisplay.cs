using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Net.NetworkInformation;

public class TooltipDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected bool onElement;
    public string content;
    public string header;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (DebugLogger.Instance.logTooltip) Debug.Log("Entered element");
        onElement = true;
        StartCoroutine(WaitAndDisplay());
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (DebugLogger.Instance.logTooltip) Debug.Log("Exited element");
        ToolTipManager.Instance.Hide();
        onElement = false;
    }

    private IEnumerator WaitAndDisplay()
    {
        yield return new WaitForSeconds(0.5f);
        if (onElement)
        {
            if (DebugLogger.Instance.logTooltip) Debug.Log("Still on element, time to display");
            this.Display();
        }
    }

    protected virtual void Display()
    {
        if (DebugLogger.Instance.logTooltip) Debug.Log("Displaying with TooltipDisplay method");
        ToolTipManager.Instance.Show(content, header);
    }
}
