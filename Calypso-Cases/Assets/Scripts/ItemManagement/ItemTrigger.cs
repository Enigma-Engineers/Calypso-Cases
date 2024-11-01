using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    private bool playerInRange;
    private GameObject visualCue;
    [SerializeField] private MageSightToggle mageSightToggle;

    private void Awake()
    {
        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EvidenceTrigger")
        {
            playerInRange = true;
            visualCue = collision.gameObject.transform.GetChild(0).gameObject;
            if (!collision.gameObject.transform.parent.GetComponent<ItemPickup>().requiresMageSight || 
                collision.gameObject.transform.parent.GetComponent<ItemPickup>().requiresMageSight && mageSightToggle.SightEnabled)
            {
                visualCue.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "EvidenceTrigger")
        {
            playerInRange = false;

            // this gets the Cue Object from the trigger
            visualCue.SetActive(false);
        }
    }
}
