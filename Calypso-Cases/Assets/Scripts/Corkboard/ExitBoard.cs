using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitBoard : MonoBehaviour
{
    [SerializeField] private SceneChange sceneChange;

    public void Start()
    {
        sceneChange = FindObjectOfType<SceneChange>();
    }
    public void onExit(InputAction.CallbackContext ctx)
    {   

        if(ctx.phase.Equals(InputActionPhase.Started))
        {
            sceneChange.ToggleScene();
        }    
    }

    public void onExit()
    {
        sceneChange.ToggleScene();
    }
}
