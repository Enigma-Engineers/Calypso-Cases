using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyInvestigation : MonoBehaviour
{
    [SerializeField]
    private GameObject bruisedHands;

    [SerializeField]
    private GameObject burnMarks;

    [SerializeField]
    private GameObject scratchMarks;

    [SerializeField] private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        bool canReceiveHandEvidence = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("checkedHand")).value;
        bool canReceiveBodyEvidence = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("checkedBody")).value;

        if (canReceiveHandEvidence && !inventory.GetInventory().Contains(bruisedHands.GetComponent<ItemPickup>()))
        {
            inventory.AddItem(bruisedHands.GetComponent<ItemPickup>());
        }

        if(canReceiveBodyEvidence && !inventory.GetInventory().Contains(scratchMarks.GetComponent<ItemPickup>()) &&
            !inventory.GetInventory().Contains(burnMarks.GetComponent<ItemPickup>()))
        {
            inventory.AddItem(scratchMarks.GetComponent<ItemPickup>());
            inventory.AddItem(burnMarks.GetComponent<ItemPickup>());
        }

    }
}
