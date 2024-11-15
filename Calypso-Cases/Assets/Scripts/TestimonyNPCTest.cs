using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestimonyNPCTest : MonoBehaviour
{
    [SerializeField]
    private GameObject testimonyEvidence;

    [SerializeField] private Inventory inventory;


    // Update is called once per frame
    void Update()
    {
        bool canReceiveTestimony = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("canReceiveTestimony")).value;

        if (canReceiveTestimony && !inventory.GetInventory().Contains(testimonyEvidence.GetComponent<ItemPickup>()))
        {
            inventory.AddItem(testimonyEvidence.GetComponent<ItemPickup>());
        }
    }
}
