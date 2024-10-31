using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private SerializedDictionary<string, GameObject> _submenus;

    public void MenuToggle(InputAction.CallbackContext ctx)
    {
        if (ctx.phase.Equals(InputActionPhase.Started))
        {
            if (_pauseMenu.activeSelf)
            {
                Resume();
                RouteMain();
            }
            else
            {
                Pause();
            }
        }
    }

    public void InventoryToggle(InputAction.CallbackContext ctx)
    {
        if (ctx.phase.Equals(InputActionPhase.Started))
        {
            if (_pauseMenu.activeSelf)
            {
                Resume();
                RouteMain();
            }
            else
            {
                RouteInventory();
                Pause();
            }
        }
    }

    /// <summary>
    /// Sets the pause menu to enabled and freezes the game
    /// </summary>
    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    /// <summary>
    /// Sets the pause menu to disabled and unfreezes the game
    /// </summary>
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Routes to base pause submenu
    /// </summary>
    public void RouteMain()
    {
        foreach (GameObject gameObject in _submenus.Values)
        {
            gameObject.SetActive(false);
        }
        _submenus["main"].SetActive(true);
    }
    /// <summary>
    /// Routes to inventory pause submenu
    /// </summary>
    public void RouteInventory()
    {
        foreach (GameObject gameObject in _submenus.Values)
        {
            gameObject.SetActive(false);
        }
        _submenus["inventory"].SetActive(true);
    }

    /// <summary>
    /// Closes the app
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && !_pauseMenu.activeSelf) Pause();
        else if (!pause && !_pauseMenu.activeSelf) Resume();
    }
}
