using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem;

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
        if (Tooltip.Instance != null) Tooltip.Instance.Hide();
        onElement = false;
    }

    private IEnumerator WaitAndDisplay()
    {
        yield return new WaitForSeconds(0.5f);
        if (onElement)
        {
            if (DebugLogger.Instance.logTooltip) Debug.Log("Still on element, time to display");
            Display();
        }
    }

    protected virtual void Display()
    {
        if (DebugLogger.Instance.logTooltip) Debug.Log("Displaying with TooltipDisplay method");
        if (Tooltip.Instance != null) Tooltip.Instance.Show(content, header);
    }
}
