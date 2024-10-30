using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private Transform player;  // Reference to the player
    private Vector3 originalPosition;           // Stores the player's original position
    private bool isTeleported = false;          // Tracks if the player is currently teleported

    // Method to be called when the "Teleport" action is triggered
    public void OnTeleport(InputAction.CallbackContext context)
    {
        // Check if the action phase is "performed" (button pressed down)
        if (context.performed)
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
