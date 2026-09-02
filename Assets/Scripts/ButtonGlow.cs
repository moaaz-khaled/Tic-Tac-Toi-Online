using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Scale")]
    public float hoverScale = 1.04f;
    public float scaleSpeed = 8f;

    [Header("Cursor")]
    public Texture2D pointerCursor;
    public Texture2D BlockedCursor;

    private bool isHovered;
    private RectTransform button;
    private Button uiButton;
    private Vector3 originalScale;

    void Awake()
    {
        button = GetComponent<RectTransform>();
        uiButton = GetComponent<Button>();

        if (button != null)
            originalScale = button.localScale;
    }

    void Update()
    {
        if (button != null)
        {
            bool canScale = isHovered && IsClickable();
            Vector3 targetScale = originalScale * (canScale ? hoverScale : 1f);
            button.localScale = Vector3.Lerp(button.localScale, targetScale, scaleSpeed * Time.unscaledDeltaTime);
        }

        if (isHovered)
            UpdateCursor();
    }

    bool IsClickable()
    {
        return uiButton == null || uiButton.interactable;
    }

    void UpdateCursor()
    {
        if (IsClickable())
        {
            Cursor.SetCursor(pointerCursor , Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(BlockedCursor , Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        UpdateCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void OnDisable()
    {
        if (isHovered)
        {
            isHovered = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}