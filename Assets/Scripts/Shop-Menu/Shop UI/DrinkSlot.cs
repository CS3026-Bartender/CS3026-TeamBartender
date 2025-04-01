using UnityEngine;

public class DrinkSlot : MonoBehaviour
{
    protected DropArea DropArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected virtual void Awake() 
    {
        DropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
		DropArea.OnDropHandler += OnIngDropped;
    }

    private void OnIngDropped(DraggableComponent draggable)
    {
        draggable.transform.position = transform.position;
    }
}
