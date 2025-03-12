using System;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
	public bool IsDropAllowed { get; set; }
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
        OnDropHandler?.Invoke(draggable);
	}
}
