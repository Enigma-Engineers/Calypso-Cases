using UnityEngine;

public class SpriteEffect : MonoBehaviour
{
    [SerializeField]
    private MageSightToggle _manager;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Color _effectColor = Color.blue;  // Change this as needed
    private Color _originalColor;

    private void Start()
    {
        _manager.SightObservers += SetSightEffect;
        _originalColor = _spriteRenderer.color;
    }

    private void OnDestroy()
    {
        _manager.SightObservers -= SetSightEffect;
    }


    /// <summary>
    /// Applies color filter to sprite
    /// </summary>
    private void ApplyEffect()
    {
        _spriteRenderer.color = _effectColor;  // Change to the effect color
    }

    /// <summary>
    /// Removes color filter from sprite
    /// </summary>
    private void RemoveEffect()
    {
        _spriteRenderer.color = _originalColor;  // Restore the original color
    }

    /// <summary>
    /// Checks wether sight is enabled or not and changes filter acordingly
    /// </summary>
    public void SetSightEffect()
    {
        if (_manager.SightEnabled) ApplyEffect();
        else RemoveEffect();
    }
}