using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
