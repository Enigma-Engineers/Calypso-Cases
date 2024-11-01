using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    private static TeleportController instance;
    private static Vector3 originalPosition;  // Tracks original position across scenes
    private static bool isTeleported = false; // Tracks teleport status

    [SerializeField] private Transform player; // Reference to the player


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
