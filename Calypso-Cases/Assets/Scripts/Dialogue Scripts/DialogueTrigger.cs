using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;

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
            collider.gameObject.transform.GetChild(0)
                .gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            playerInRange = false;

            // this gets the Cue Object from the trigger
            collider.gameObject.transform.GetChild(0)
                .gameObject.SetActive(false);
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (playerInRange)
        {
            if (ctx.phase.Equals(InputActionPhase.Started))
            {
                Debug.Log(inkJSON.text);
            }
        }
    }
}
