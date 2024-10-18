using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitBoard : MonoBehaviour
{
    public void onExit(InputAction.CallbackContext ctx)
    {
        if(ctx.phase.Equals(InputActionPhase.Started))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }    
    }
}
