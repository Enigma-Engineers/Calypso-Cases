using UnityEngine;

public class SpriteEffect : MonoBehaviour, ISightObserver
{
    [SerializeField]
    private MageSightToggle _manager;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Color _effectColor = Color.blue;  // Change this as needed
    private Color _originalColor;

    public MageSightToggle Manager => _manager;

    public void Subscribe()
    {
        Manager.SightObservers += SetSightEffect;
    }

    public void Unsubscribe()
    {
        Manager.SightObservers -= SetSightEffect;
    }

    /// <summary>
    /// Sets the sight effect based on the MageSight status.
    /// </summary>
    public void SetSightEffect()
    {
        if (_manager.SightEnabled) ApplyEffect();
        else RemoveEffect();
    }

    private void Start()
    {
        Subscribe();
        _originalColor = _spriteRenderer.color;
    }

    /// <summary>
    /// Applies color filter to sprite.
    /// </summary>
    private void ApplyEffect()
    {
        _spriteRenderer.color = _effectColor;  // Change to the effect color

    }

    /// <summary>
    /// Removes color filter from sprite.
    /// </summary>
    private void RemoveEffect()
    {
        _spriteRenderer.color = _originalColor;  // Restore the original color
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}