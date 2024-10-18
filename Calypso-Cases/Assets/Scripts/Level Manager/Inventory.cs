using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<string> items = new List<string>();   // Stores the names of the items the player has picked up

    // Adds an item to the inventory
    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Item added: " + itemName);
    }

    // Example method to display all items in the inventory (can be expanded later)
    public List<string> GetInventory()
    {
        foreach (string item in items)
        {
            Debug.Log("Inventory Item: " + item);
        }
        return items;
    }

    public void SetInventory(List<string>inv)
    {
        items = inv;
    }
}
