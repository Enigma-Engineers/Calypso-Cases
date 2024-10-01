using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SightToggle : MonoBehaviour
{
    private bool _sight;
    // Start is called before the first frame update
    private void Start()
    {
        _sight = false;
    }

    public void OnToggle(InputAction.CallbackContext ctx)
    {
        if(ctx.phase.Equals(InputActionPhase.Started))
        {
            _sight = !_sight;
            Debug.Log($"Sight enabled = {_sight}");
        }
    }
}
