using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;
    private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    private void Awake()
    {
        playerInRange = false;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            playerInRange = true;

            // this gets the Cue Object from the trigger
            visualCue = collider.gameObject.transform.GetChild(0).gameObject;

            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            playerInRange = false;

            // this gets the Cue Object from the trigger
            visualCue.SetActive(false);
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
            }
        }
    }
}
