using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffect : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // Variables for effect customization
    [SerializeField]
    private Color effectColor = Color.blue;  // Change this as needed
    private Color originalColor;

    // Called when the script starts
    void Start()
    {
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;  // Store the original color of the sprite
        }
    }

    // Applies the effect
    void ApplyEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = effectColor;  // Change to the effect color
        }
    }

    // Removes the effect (restores the original state)
    void RemoveEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;  // Restore the original color
        }
    }

    // Takes a boolean and applies or removes the effect based on the value
    public void SetSight(bool on)
    {
        if (on)
        {
            ApplyEffect();  // Apply the effect when "on" is true
        }
        else
        {
            RemoveEffect();  // Remove the effect when "on" is false
        }
    }
}