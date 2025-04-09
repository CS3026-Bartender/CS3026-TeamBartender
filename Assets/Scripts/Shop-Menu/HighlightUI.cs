using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;

public class HighlightUI : MonoBehaviour
{
    [SerializeField] private Image image;

    private Color originalcolor;

    private void Start()
    {
        originalcolor = image.color;
    }

    public void HighlightValid()
    {
        image.color = new Color32(255, 223, 115, 255);
    }

    public void HighlightInvalid()
    {
        image.color = new Color32(198, 38, 25, 255);
    }

    public void DehighlightImage()
    {
        image.color = originalcolor;
    }
}
