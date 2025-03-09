using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class IngText : MonoBehaviour
{
    public TextMeshProUGUI TextField;
    public Image image;

    public void UpdateText(string newText) 
    {
        TextField.text = newText;
    }

    public void UpdateImage(Image newImage)
    {
        image = newImage;
    }

}
