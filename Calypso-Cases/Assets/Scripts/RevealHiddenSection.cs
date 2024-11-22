using System.Collections;
using UnityEngine;

public class RevealHiddenSection : MonoBehaviour
{
    [SerializeField] private GameObject blackCover; // Assign your 2D object here
    [SerializeField] private float fadeSpeed = 2f;

    private bool isFading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;

        SpriteRenderer renderer = blackCover.GetComponent<SpriteRenderer>();
        Color color = renderer.color;

        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            renderer.color = color;
            yield return null;
        }

        blackCover.SetActive(false); // Hide the object completely after fading
        isFading = false;
    }
}
