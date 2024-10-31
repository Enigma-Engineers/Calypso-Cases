using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool IsPaused = false;

    [SerializeField]
    private GameObject pauseMenuCanvas;

    public void MenuToggle(InputAction.CallbackContext ctx)
    {
        // if dialogue is not playing upon hitting exit
        // go to pause menu
        if (ctx.phase.Equals(InputActionPhase.Started)
            && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }
    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && !IsPaused) Pause();
        else if (!pause && !IsPaused) Resume();
    }
}
