using System;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
	public bool IsDropAllowed { get; set; } = true;
    public List<DropCondition> DropConditions = new List<DropCondition>();
	public event Action<DraggableComponent> OnDropHandler;

	public bool Accepts(DraggableComponent draggable)
	{
		if (IsDropAllowed)
		{
            return DropConditions.TrueForAll(cond => cond.Check(draggable));
        }
		else
		{
			return false;
		}
	}

	public void Drop(DraggableComponent draggable)
	{
        if (DebugLogger.Instance.logDragAndDrop) Debug.Log("Drop area received item");

		//draggable.transform.SetParent(transform, false);
		//draggable.transform.position = transform.position;

		// Complete purchase with this drink & slot
        int drink = transform.parent.GetSiblingIndex();
        int slot = transform.GetSiblingIndex();
        DrinkPurchaseManager.Instance.CompletePurchase(drink, slot);

        OnDropHandler?.Invoke(draggable);
	}
}
