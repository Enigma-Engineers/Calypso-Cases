using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    private static TeleportController instance;
    private static Vector3 originalPosition;  // Tracks original position across scenes
    private static bool isTeleported = false; // Tracks teleport status

    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private string mainSceneName = "MainArea"; // Main scene name for teleporting

    private void Awake()
    {
        // Ensure only one instance of the TeleportController exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Check if player already exists in the "DontDestroyOnLoad" area
        if (player != null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            // If multiple player instances are found, keep only the one already in "DontDestroyOnLoad"
            foreach (var obj in players)
            {
                if (obj != player.gameObject)
                {
                    Destroy(obj);
                }
            }

            DontDestroyOnLoad(player.gameObject);
        }
        else
        {
            Debug.LogError("Player reference is missing in TeleportController.");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure the player reference remains after scene change
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
                DontDestroyOnLoad(player.gameObject);
            }
        }
        else
        {
            // Destroy any new duplicate player instances that may appear in the scene
            foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (obj != player.gameObject)
                {
                    Destroy(obj);
                }
            }
        }
    }

    // Method to handle the "Teleport" action
    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (context.performed && player != null)
        {
            ToggleTeleport();
        }
    }

    private void ToggleTeleport()
    {
        if (isTeleported)
        {
            // Teleport back to the original position
            player.position = originalPosition;
            isTeleported = false;
        }
        else
        {
            // Save the player's current position and teleport to this object's position
            originalPosition = player.position;
            player.position = transform.position;
            isTeleported = true;
        }
    }
}
