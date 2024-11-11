using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsFadeinAndOut : MonoBehaviour
{
    [SerializeField] Fader fader;
    [SerializeField] float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    public void Update()
    {
        // If the player has moved, fade the controls out
        // Then destroy it
        if (FindAnyObjectByType<PlayerMovement>().HasMoved == true)
        {
            FadeOut();
            if (fader.CanvasGroup.alpha == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Use fader to fade out
    private void FadeIn()
    {
        fader.Fade(1, fadeDuration);
    }

    // Use fader to fade out
    private void FadeOut()
    {
        fader.Fade(0, fadeDuration);
    }
}
