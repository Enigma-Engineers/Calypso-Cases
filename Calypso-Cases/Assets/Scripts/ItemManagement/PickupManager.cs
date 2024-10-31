using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupManager : MonoBehaviour
{
    private List<ItemPickup> pickupableItems = new List<ItemPickup>();

    [SerializeField] private GameObject player;
    [SerializeField] private float pickupRange = 2f;
    [SerializeField] private MageSightToggle mageSightToggle;
    [SerializeField] private GameObject levelManager;
    private PlayerInput playerInput;
    private Inventory inventory;

    private void Awake()
    {
        // Try to get the PlayerInput from the player GameObject, if it's null, log an error
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();

            if (playerInput != null)
            {
                playerInput.actions["Pickup"].performed += OnPickup;
            }
            else
            {
                Debug.LogError("PlayerInput component is missing on the player object.");
            }
        }
        else
        {
            Debug.LogError("Player object reference is missing in the PickupManager.");
        }
    }

    // Add the item to the manager
    public void RegisterItem(ItemPickup item)
    {
        pickupableItems.Add(item);
    }

    // Remove the item from the manager
    public void UnregisterItem(ItemPickup item)
    {
        pickupableItems.Remove(item);
    }

    // Called when "E" is pressed
    public void OnPickup(InputAction.CallbackContext context)
    {
        // Check if player input is valid before proceeding
        if (playerInput == null) return;

        foreach (var item in pickupableItems)
        {
            if (Vector2.Distance(player.transform.position, item.transform.position) <= pickupRange)
            {
                // If the item requires Mage Sight and Mage Sight is not enabled, don't allow the pickup
                if (item.RequiresMageSight && !mageSightToggle.SightEnabled)
                {
                    Debug.Log("Mage Sight is required to pick up this item.");
                    continue;
                }

                item.PickUp();
                levelManager = GameObject.Find("LevelManager");
                inventory = levelManager.GetComponent<Inventory>();
                inventory.AddItem(item);
                break;  // Optional: only allow picking up one item at a time
            }

        }
    }

    private void OnDestroy()
    {
        // Ensure we unsubscribe only if playerInput is valid
        if (playerInput != null)
        {
            playerInput.actions["Pickup"].performed -= OnPickup;
        }
    }
}
