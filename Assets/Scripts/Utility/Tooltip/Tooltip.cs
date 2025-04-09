using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform rectTransform;
    private Canvas canvas;
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    public static Tooltip Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        Hide();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;
        
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

    }
    public void Show(string content, string header = "")
    {
        rectTransform.anchoredPosition = Mouse.current.position.ReadValue() / canvas.scaleFactor;
        SetText(content, header);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Mouse.current.position.ReadValue() / canvas.scaleFactor;
    }
}
