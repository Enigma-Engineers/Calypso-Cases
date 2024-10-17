using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private bool requiresMageSight = false;
    [SerializeField] private string itemName;
    [SerializeField] private GameObject player;
    [SerializeField] private PickupManager pickupManager; // Reference to the PickupManager

    public bool RequiresMageSight => requiresMageSight;

    private void Start()
    {
        // Register the item with the PickupManager
        pickupManager = FindObjectOfType<PickupManager>();
        if (pickupManager != null)
        {
            pickupManager.RegisterItem(this);
        }
        else
        {
            Debug.LogError("PickupManager not found in the scene!");
        }
    }

    public void PickUp()
    {
        Debug.Log($"{itemName} picked up!");
        // Add the item to the player's inventory
        player.GetComponent<Inventory>().AddItem(itemName);
        gameObject.SetActive(false);  // Make the item disappear
    }

    private void OnDestroy()
    {
        // Unregister the item from the PickupManager when destroyed
        if (pickupManager != null)
        {
            pickupManager.UnregisterItem(this);
        }
    }
}
