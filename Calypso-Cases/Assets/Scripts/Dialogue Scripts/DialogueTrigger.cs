using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;
    private GameObject visualCue;

    private TextAsset inkJSON;

    private void Awake()
    {
        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            // Get the Mage Sight Manager's Sight Toggle Component
            MageSightToggle mageSight = FindObjectOfType<MageSightToggle>();
            playerInRange = true;

            // this gets the Cue Object from the trigger
            visualCue = collider.gameObject.transform.GetChild(0).gameObject;

            visualCue.SetActive(true);

            // Set the inkJSON to the NPC's text JSON
            if (!mageSight.SightEnabled)
            {
                inkJSON = collider.gameObject.GetComponent<NPCTextTrigger>().DefaultInkJSON;
            }

            // If the player is using Sight and the NPC has a corresponding JSON
            // Set the inkJSON to the NPC's text JSON
            else if (mageSight.SightEnabled &&
                collider.gameObject.GetComponent<NPCTextTrigger>().MagicInkJSON != null) 
            {
                inkJSON = collider.gameObject.GetComponent<NPCTextTrigger>().MagicInkJSON;
            }

            // If the player is using Sight but the NPC doesn't have a corresponding 
            // JSON, Set the inkJSON to the Default JSON
            else
            {
                inkJSON = collider.gameObject.GetComponent<NPCTextTrigger>().DefaultInkJSON;
            }
         
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            playerInRange = false;

            // this gets the Cue Object from the trigger
            visualCue.SetActive(false);

            // set inkJSON to null
            inkJSON = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (ctx.phase.Equals(InputActionPhase.Started))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                // disables the visual cue
                visualCue.SetActive(false);
                Debug.Log("Dialogue Entered");
            }
        }
    }
}
