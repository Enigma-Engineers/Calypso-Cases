using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    private bool hasWon = false;
    [SerializeField] private CaseData caseData;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject wrongCanvas;
    private Inventory inventory;
    [SerializeField] private Image fadeOverlay;  // UI Image for the fade effect
    [SerializeField] private float fadeDuration = 1.0f;  // Duration of the fade effect


    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }
    private void Update()
    {

    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeOverlay.color;
        color.a = 0f;  // Start with a fully transparent image
        fadeOverlay.color = color;

        // Fade to opaque
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeOverlay.color = color;
            yield return null;
        }

        // Load the next scene
        SceneManager.LoadScene(sceneName);
    }

    public void CheckWin()
    {

        hasWon = caseData.Compare();
        if(hasWon) {
            winCanvas.SetActive(true);
            inventory.ClearInventory();
            StartCoroutine(FadeAndLoadScene("Level_2"));
        }
        else
        {
            StartCoroutine(IncorrectEvidence());
        }
    }

    private IEnumerator IncorrectEvidence()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 3)
        {
            elapsedTime += Time.deltaTime;
            wrongCanvas.SetActive(true);
            yield return null;
        }
        wrongCanvas.SetActive(false);
    }
}
