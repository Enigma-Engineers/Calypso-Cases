using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // The index's used to access a Scene
    [SerializeField] private int mainSceneIndex = 0;
    [SerializeField] private int secondarySceneIndex = 1;

    // This boolean keeps track of which scene is currently active
    // If the secondary scene is active, then it's true
    // if it is not, the main scene is currently active
    private bool isSecondarySceneActive = false;

    // Call this method to toggle between the main and secondary scenes
    public void ToggleScene()
    {
        if (isSecondarySceneActive)
        {
            // If the secondary scene is active
            // deactivate it and activate the main scene
            DeactivateSecondaryScene();
            ActivateMainScene();
        }
        else
        {
            // Otherwise, do the reverse and deactivate
            // The main scene and activate the secondary scene
            DeactivateMainScene();
            ActivateSecondaryScene();
        }
    }

    // Activates main scene by enabling all its root GameObjects
    private void ActivateMainScene()
    {
        SetSceneActive(mainSceneIndex, true);
        isSecondarySceneActive = false;
    }

    // Deactivates main scene by disabling all root GameObjects
    private void DeactivateMainScene()
    {
        SetSceneActive(mainSceneIndex, false);
    }

    // Loads secondary scene additively and activates it by enabling
    // Root GameObjects
    private void ActivateSecondaryScene()
    {
        SceneManager.LoadScene(secondarySceneIndex, LoadSceneMode.Additive);
        SetSceneActive(secondarySceneIndex, true);
        isSecondarySceneActive = true;
    }

    // Unloads the secondary scene, returning to the main scene
    private void DeactivateSecondaryScene()
    {
        SceneManager.UnloadSceneAsync(secondarySceneIndex);
        isSecondarySceneActive = false;
    }

    // Helper method to set all root objects in a scene to active or inactive
    private void SetSceneActive(int sceneIndex, bool isActive)
    {
        // retrieve Scene by index
        Scene scene = SceneManager.GetSceneByBuildIndex(sceneIndex);

        // Check to see if it's loaded
        if (scene.IsValid())
        {
            // Loop through all root objects and set their active state
            foreach (GameObject obj in scene.GetRootGameObjects())
            {
                obj.SetActive(isActive);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ToggleScene();
        }
    }
}
