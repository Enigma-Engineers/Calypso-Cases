using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBlocker : MonoBehaviour
{
    private bool isCollidingWithPlayer = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding is the player (using Unity's GameObject Tag system)
        if (other.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // When the player leaves, set the flag to false
        if (other.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
        }
    }

    // Method to check if the player is currently blocked
    public bool IsPlayerBlocked()
    {
        return isCollidingWithPlayer;
    }
}