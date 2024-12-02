using System.Collections;
using UnityEngine;

public class RevealHiddenSection : MonoBehaviour
{
    [SerializeField] private GameObject blackCover; // Assign your 2D object here
    [SerializeField] private float fadeSpeed = 2f;

    private bool isFading = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            Debug.Log("Player entered the trigger area."); // Debugging
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;

        SpriteRenderer renderer = blackCover.GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            Debug.LogError("No SpriteRenderer found on blackCover!");
            yield break;
        }

        Color color = renderer.color;

        while (color.a > 0)
        {
            Debug.Log($"Fading... Current Alpha: {color.a}"); // Debugging
            color.a -= Time.deltaTime * fadeSpeed;
            renderer.color = color;
            yield return null;
        }

        Debug.Log("Fade complete. Hiding black cover."); // Debugging
        blackCover.SetActive(false); // Hide the object completely after fading
        isFading = false;
    }
}
