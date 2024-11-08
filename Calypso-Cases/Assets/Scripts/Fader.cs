using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup { get { return canvasGroup; } }

    private void Awake()
    {
        // get or add a CanvasGroup component to the object
        canvasGroup = GetComponent<CanvasGroup>();

        if(canvasGroup == null )
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void Fade(float targetAlpha,  float duration)
    {
        StartCoroutine(FadeCoroutine(targetAlpha, duration));
    }

    private IEnumerator FadeCoroutine(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / duration);
            yield return null;
        }
 

        canvasGroup.alpha = targetAlpha;

    }
}
