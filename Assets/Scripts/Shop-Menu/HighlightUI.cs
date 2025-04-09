using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;

public class HighlightUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Color highlight;

    private Color originalcolor;

    private void Start()
    {
        originalcolor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        image.color = highlight;
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        image.color = originalcolor;
    }
}
