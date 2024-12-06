using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MirrorRevealTrigger : MonoBehaviour
{
    [Header("Fader Settings")]
    [SerializeField] private Fader fader; // Assign your Fader object
    [SerializeField] private float fadeDuration = 1f; // Duration of the fade

    [Header("Game Objects to Disable")]
    [SerializeField] private GameObject[] objectsToDisable; // List of objects to disable

    private bool isTriggered = false;
    private DialogueManager dialogueManager;

    private void Start()
    {
        // Get the instance of the DialogueManager in the scene
        dialogueManager = DialogueManager.GetInstance();

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene. Please ensure it exists.");
        }
    }

    // Call this method when the magic mirror dialogue starts
    public void TriggerMagicMirrorEffect()
    {
        if (isTriggered || dialogueManager == null) return;

        isTriggered = true;
        StartCoroutine(HandleMagicMirror());
    }

    private IEnumerator HandleMagicMirror()
    {
        // Wait until dialogue is no longer playing
        yield return new WaitUntil(() => !dialogueManager.dialogueIsPlaying);

        // Start fading to black
        fader.Fade(1, fadeDuration);

        // Wait for the fade to complete
        yield return new WaitForSeconds(fadeDuration);

        // Disable the specified objects
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Fade back in
        fader.Fade(0, fadeDuration);
    }
}