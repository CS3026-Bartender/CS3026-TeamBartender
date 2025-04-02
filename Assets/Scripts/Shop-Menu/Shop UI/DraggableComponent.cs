using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<PointerEventData> OnBeginDragHandler;
	public event Action<PointerEventData> OnDragHandler;
	public event Action<PointerEventData, bool> OnEndDragHandler;
	public bool FollowCursor { get; set; } = true;
	public Vector3 StartPosition;
	public bool CanDrag { get; set; } = true;

    private RectTransform rectTransform;
	private Canvas canvas;

    private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (DebugLogger.Instance.logDragAndDrop) Debug.Log("Begin drag");
        if (!CanDrag)
        {
            if (DebugLogger.Instance.logDragAndDrop) Debug.Log("Oops, can't drag");
            return;
        }

        DrinkPurchaseManager.Instance.StartPurchase(transform.GetSiblingIndex());
        OnBeginDragHandler?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CanDrag) {return;}
        
        OnDragHandler?.Invoke(eventData);
        if (FollowCursor) {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DebugLogger.Instance.logDragAndDrop) Debug.Log("End drag");
        if (!CanDrag)
        {
            if (DebugLogger.Instance.logDragAndDrop) Debug.Log("Oops, couldn't drag");
            return;
        }
        
        var results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
        if (DebugLogger.Instance.logDragAndDrop) Debug.Log("Raycast got " + results.Count + " results");

        DropArea dropArea = null;

		foreach (var result in results)
		{
			dropArea = result.gameObject.GetComponent<DropArea>();

			if (dropArea != null)
            {
                if (DebugLogger.Instance.logDragAndDrop) Debug.Log("Found drop area");
                break;
            }
		}

		if (dropArea != null)
		{
			if (dropArea.Accepts(this))
			{
                if (DebugLogger.Instance.logDragAndDrop) Debug.Log("Drop area is valid, dropping");
                dropArea.Drop(this);
				OnEndDragHandler?.Invoke(eventData, true);
				return;
			}
		}

        if (DebugLogger.Instance.logDragAndDrop) Debug.Log("Drop area is not valid, canceling");

        DrinkPurchaseManager.Instance.CancelPurchase();
        rectTransform.anchoredPosition = StartPosition;
		OnEndDragHandler?.Invoke(eventData, false);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        StartPosition = rectTransform.anchoredPosition;
    }
}
