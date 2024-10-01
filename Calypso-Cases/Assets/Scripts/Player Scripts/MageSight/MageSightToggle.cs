using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MageSightToggle : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<bool> _sightToggleObserver;

    private bool _enabled = false;

    /// <summary>
    /// Toggles mage sight on/off
    /// </summary>
    /// <param name="ctx"></param>
    public void Toggle(InputAction.CallbackContext ctx)
    {
        if(ctx.phase.Equals(InputActionPhase.Started))
        {
            _enabled = !_enabled;
            _sightToggleObserver.Invoke(_enabled);
        }
    }
}
