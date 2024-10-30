using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    private Color highlightColor = new Color(0f, 0f, 1f, 0.95f); // Light transparent blue

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    private void OnMouseEnter()
    {
        objectRenderer.material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        objectRenderer.material.color = originalColor;
    }
}
