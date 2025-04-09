using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class HighlightUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Color highlight;

    private Color originalcolor;

    private void Start()
    {
        highlight.a = 1;
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        image.tintColor = highlight;
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        image.tintColor = originalcolor;
    }
}
