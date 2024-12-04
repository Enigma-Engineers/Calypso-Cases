using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTrigger : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField]
    private GameObject visualCue;

    private void Awake()
    {
        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Mirror")
        {
            playerInRange = true;

            // this gets the Cue Object from the trigger
            visualCue = collider.gameObject.transform.GetChild(0).gameObject;

            visualCue.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Mirror")
        {
            playerInRange = false;

            // this gets the Cue Object from the trigger
            visualCue.SetActive(false);
        }
    }
}
