using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup startScreenCanvasGroup;
    [SerializeField] private float fadeDuration;

    [SerializeField] private TextAsset inkJSON;

    private bool isFading = false;

    private void Start()
    {
        startScreenCanvasGroup.alpha = 1;
    }

    public void StartGame()
    {
        if(!isFading)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        isFading = true;

        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            startScreenCanvasGroup.alpha = Mathf.Lerp(1, 0, timer/fadeDuration);
            yield return null;  
        }

        Debug.Log("Play Dialogue");
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        DialogueManager.GetInstance().ContinueStory();
    }
}
