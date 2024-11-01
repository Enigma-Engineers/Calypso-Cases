using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public bool requiresMageSight = false;
    [SerializeField] public string itemName;
    [SerializeField] public string description;
    [SerializeField] public int index;
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

    public ItemPickup(string name, string desc, int ind, bool magic)
    {
        itemName = name;
        description = desc;
        index = ind;
        requiresMageSight |= magic;
    }

    public void PickUp()
    {
        Debug.Log($"{itemName} picked up!");
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