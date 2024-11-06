using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private CanvasGroup startScreenCanvasGroup;
    [SerializeField] private float fadeDuration;

    private bool isFading = false;

    private void Start()
    {
        if (!isFading)
        {
            startScreenCanvasGroup.alpha = 1;
            FindObjectOfType<PlayerMovement>().CanMove = false;
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            startScreenCanvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }

        FindObjectOfType<PlayerMovement>().CanMove = true;

        Destroy(gameObject);
    }
}
