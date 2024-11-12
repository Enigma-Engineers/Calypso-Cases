using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestimonyNPCTest : MonoBehaviour
{
    private ItemPickup testimony;

    [SerializeField] private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        testimony = new ItemPickup("testimony", "This is a testimony", 0, false);
    }

    // Update is called once per frame
    void Update()
    {
        bool canReceiveTestimony = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("canReceiveTestimony")).value;

        if (canReceiveTestimony && !inventory.GetInventory().Contains(testimony))
        {
            inventory.AddItem(testimony);
        }
    }
}
