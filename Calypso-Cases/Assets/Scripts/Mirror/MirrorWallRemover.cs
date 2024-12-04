using System.Collections;
using UnityEngine;

public class MirrorWallRemover : MonoBehaviour
{
    [SerializeField] private GameObject mirror; // Reference to the mirror GameObject
    [SerializeField] private GameObject wall;   // Reference to the wall GameObject
    [SerializeField] private TextAsset offDialogueJSON; // Dialogue to play when interacting without Sight
    [SerializeField] private TextAsset onDialogueJSON; // Dialogue to play when interacting with Sight

    [SerializeField]
    private bool interactionTriggered = false;

    // Reference to MageSightToggle (make sure this is assigned in the Inspector or fetched dynamically)
    [SerializeField] private MageSightToggle mageSightToggle;

    private void Start()
    {
        // Ensure that MageSightToggle is assigned if not already in Inspector
        if (mageSightToggle == null)
        {
            mageSightToggle = FindObjectOfType<MageSightToggle>(); // Dynamically find MageSightToggle if not set
        }
    }

    private void Update()
    {
        // Detect interaction (e.g., using Input.GetKey or a custom interaction system)
        if (Input.GetKeyDown(KeyCode.E) && !interactionTriggered) // Replace with your interaction key
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        // Ensure mageSightToggle is assigned and SightEnabled is true before proceeding
        if (mageSightToggle != null)
        {

            if (mirror != null && DialogueManager.GetInstance() != null)
            {
                interactionTriggered = true;

                // Start dialogue
                if (!mageSightToggle.SightEnabled)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(offDialogueJSON);
                    DialogueManager.GetInstance().ContinueStory();
                }
                else
                {
                    DialogueManager.GetInstance().EnterDialogueMode(onDialogueJSON);
                    DialogueManager.GetInstance().ContinueStory();
                }


                // Start coroutine to wait for dialogue to finish
                StartCoroutine(WaitForDialogueToFinish());
            }
            else
            {
                Debug.LogWarning("Missing references or DialogueManager instance!");
            }
        }
        else
        {
            Debug.Log("MageSight is not enabled, cannot interact.");
        }
    }

    private IEnumerator WaitForDialogueToFinish()
    {
        // Wait until the dialogue is no longer active
        while (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            yield return null; // Wait for the next frame

        }

        interactionTriggered = false;

        // Destroy the mirror and wall
        if (mageSightToggle.SightEnabled)
        {
            if (mirror != null) Destroy(mirror);
            if (wall != null) Destroy(wall);
        }

        Debug.Log("Mirror and wall removed after dialogue!");
    }
}
