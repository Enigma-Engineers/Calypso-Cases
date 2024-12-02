using System.Collections;
using UnityEngine;

public class MirrorWallRemover : MonoBehaviour
{
    [SerializeField] private GameObject mirror; // Reference to the mirror GameObject
    [SerializeField] private GameObject wall;   // Reference to the wall GameObject
    [SerializeField] private TextAsset dialogueJSON; // Dialogue to play when interacting

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
        if (mageSightToggle != null && mageSightToggle.SightEnabled)
        {
            if (mirror != null && DialogueManager.GetInstance() != null && dialogueJSON != null)
            {
                interactionTriggered = true;

                // Start dialogue
                DialogueManager.GetInstance().EnterDialogueMode(dialogueJSON);

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

        // Destroy the mirror and wall
        if (mirror != null) Destroy(mirror);
        if (wall != null) Destroy(wall);

        Debug.Log("Mirror and wall removed after dialogue!");
    }
}
