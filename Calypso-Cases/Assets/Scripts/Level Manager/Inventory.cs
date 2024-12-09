using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action ItemAdded;

    [SerializeField]
    public List<ItemPickup> items = new List<ItemPickup>();   // Stores the names of the items the player has picked up

    // Adds an item to the inventory
    public void AddItem(ItemPickup evidence)
    {
        items.Add(evidence);
        ItemAdded.Invoke();
        Debug.Log("Item added: " + evidence.name);
    }

    // Example method to display all items in the inventory (can be expanded later)
    public List<ItemPickup> GetInventory()
    {
        foreach (ItemPickup item in items)
        {
            Debug.Log("Inventory Item: " + item.itemName);
        }
        return items;
    }

    public void SetInventory(List<ItemPickup>inv)
    {
        items = inv;
    }

    public void ClearInventory()
    {
        items = new List<ItemPickup>();
    }
}
