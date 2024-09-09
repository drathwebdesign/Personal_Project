using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Button button;
    private Color originalColor;
    private Vector3 originalScale;

    public Color hoverColor = Color.white;  // The color to change to when hovered
    public float scaleFactor = 1.1f;  // The factor by which to scale the button when hovered
    public Texture2D hoverCursorTexture;  // Texture for the hover cursor
    private Texture2D originalCursorTexture;  // To store the original cursor

    void Start() {
        button = GetComponent<Button>();
        originalColor = button.image.color;  // Store the original color of the button
        originalScale = transform.localScale;  // Store the original scale of the button
        originalCursorTexture = null;  // Original cursor is null, meaning the default system cursor
    }

    // Method called when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData) {
        button.image.color = hoverColor;  // Change to hover color
        transform.localScale = originalScale * scaleFactor;  // Scale up the button
        Cursor.SetCursor(hoverCursorTexture, Vector2.zero, CursorMode.Auto);  // Change cursor
    }

    // Method called when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData) {
        button.image.color = originalColor;  // Revert to original color
        transform.localScale = originalScale;  // Revert to original scale
        Cursor.SetCursor(originalCursorTexture, Vector2.zero, CursorMode.Auto);  // Revert to original cursor
    }
}