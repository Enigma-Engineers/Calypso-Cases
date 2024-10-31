using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PickupManager : MonoBehaviour
{
    private static PickupManager instance;

    private List<ItemPickup> pickupableItems = new List<ItemPickup>();

    [SerializeField] private GameObject player;
    [SerializeField] private float pickupRange = 2f;
    [SerializeField] private MageSightToggle mageSightToggle;

    private PlayerInput playerInput;

    private void Awake()
    {
        // Ensure only one instance of PickupManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Try to get the PlayerInput from the player GameObject
        InitializePlayer();

        // Subscribe to the Pickup action
        if (playerInput != null)
        {
            playerInput.actions["Pickup"].performed += OnPickup;
        }
    }

    private void InitializePlayer()
    {
        // Check if player is assigned; if not, find the player by tag
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer;
                playerInput = player.GetComponent<PlayerInput>();
            }
            else
            {
                Debug.LogError("Player object reference is missing in the PickupManager.");
                return;
            }
        }
        else
        {
            playerInput = player.GetComponent<PlayerInput>();
        }

        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing on the player object.");
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
        InitializePlayer();
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
