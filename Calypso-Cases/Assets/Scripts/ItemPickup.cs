using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private bool requiresMageSight = false;
    [SerializeField] private string itemName;
    private GameObject levelManager;
    [SerializeField] private PickupManager pickupManager; // Reference to the PickupManager

    public bool RequiresMageSight => requiresMageSight;

    private void Start()
    {
        // Register the item with the PickupManager
        levelManager = GameObject.Find("LevelManager");
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
        levelManager.GetComponent<Inventory>().AddItem(itemName);
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
