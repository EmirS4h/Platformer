using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headerField;
    [SerializeField] TextMeshProUGUI contentField;
    [SerializeField] LayoutElement layoutElement;
    [SerializeField] int characterWrapLimit;

    private int headerLength;
    private int contentLength;

    Vector2 position;

    RectTransform rectTransform;

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
            headerField.SetText(header);
        }
        contentField.SetText(content);

        headerLength = headerField.text.Length;
        contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }

    private void Update()
    {
        position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        SetPosition(position);
    }

    private void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }
}
