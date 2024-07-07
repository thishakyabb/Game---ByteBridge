using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip: MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement LayoutElement;
    public int characterWrapLimit;
    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;
 
        //should only enable when there is a specific amount of characters available
        LayoutElement.enabled =
            (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;       contentField.text = content;
    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;
        transform.position = position;
        float pivotX = position.x / Screen.width;
        float pivotY = position.y/ Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
    }
}