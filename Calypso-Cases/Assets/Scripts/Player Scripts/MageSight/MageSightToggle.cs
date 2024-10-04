using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MageSightToggle : MonoBehaviour
{
    public event Action SightObservers;

    private bool _enabled = false;

    public bool SightEnabled => _enabled;

    /// <summary>
    /// Toggles mage sight on/off
    /// </summary>
    /// <param name="ctx"></param>
    public void Toggle(InputAction.CallbackContext ctx)
    {
        if(ctx.phase.Equals(InputActionPhase.Started))
        {
            _enabled = !_enabled;
            SightObservers.Invoke();
        }
    }
}
