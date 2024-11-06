using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingIntoScene : MonoBehaviour
{
    [SerializeField] Fader fader;
    [SerializeField] float fadeDuration;

    // will prevent player from moving and fade out on beginning of the level
    public void Start()
    {
        FindObjectOfType<PlayerMovement>().CanMove = false;
        FadeOut();
    }

    public void Update()
    {
        // Once it's no longer visible, Destroy it
        if(fader.CanvasGroup.alpha == 0)
        {
            FindAnyObjectByType<PlayerMovement>().CanMove = true;
            Destroy(gameObject);    
        }
    }

    // Use fader to fade out
    private void FadeOut()
    {
        fader.Fade(0, fadeDuration);
    }
}
