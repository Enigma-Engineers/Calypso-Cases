using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaseData : MonoBehaviour
{
    private List<string> caseOne = new List<string>();
    private List<string> caseTwo = new List<string>();
    private List<string> caseTwoOptional = new List<string>();
    private List<string> caseThree = new List<string>();
    private List<string> currentCase = new List<string>();
    private List<string> currentOptional = new List<string>();
    private int currentScene;

    private List<string> userThreads = new List<string>();
    [SerializeField] private Threads threads;

    private GameObject levelManager;
    private SceneChange sceneChange;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager");
        sceneChange = levelManager.GetComponent<SceneChange>();

        caseOne.Add("Glass-Water");

        caseTwo.Add("Brass Knuckles-Bruised Knuckles");
        caseTwo.Add("Wall Scratch Marks-Body Scratch Marks");
        caseTwo.Add("Wall Scorch Marks-Body Scorch Marks");

        caseTwoOptional.Add("Wall Scratch Marks-Bodyguard's Testimony");
        caseTwoOptional.Add("Body Scratch Marks-Bodyguard's Testimony");
        caseTwoOptional.Add("Wall Scorch Marks-Bartender's Testimony");
        caseTwoOptional.Add("Body Scorch Marks-Bartender's Testimony");

        caseThree.Add("CEO's Testimony-Painting");
        caseThree.Add("CEO's Testimony-Empty Box");
        caseThree.Add("Glass Shards-Empty Box");
        caseThree.Add("Footprints-Painting");

        currentScene = sceneChange.GetCurrentScene();
    }

    private void Update()
    {   
    }

    public bool Compare()
    {
        userThreads = threads.getThreads();
        int totalCorrect = 0;
        int prevTotal = 0;
        bool notIncluded = false;
        bool correctAnswer = false;
        
        switch (currentScene)
        {
            case 1: 
                currentCase = caseOne;
                break;
            case 3:
                currentCase = caseTwo;
                currentOptional = caseTwoOptional;
                break;
            case 4:
                currentCase = caseThree;
                break;
        }

        //goes through each saved thread in the key
        foreach(string keyThread in currentCase) {
            prevTotal = totalCorrect;
            for (int i = 0; i < userThreads.Count; i += 2)
            {
                if(i + 1 < userThreads.Count)
                {
                    if (keyThread.Equals($"{userThreads[i]}-{userThreads[i + 1]}") || keyThread.Equals($"{userThreads[i + 1]}-{userThreads[i]}"))
                    {
                        totalCorrect++;
                    }
                }
            }
            if (totalCorrect == prevTotal) { 
                notIncluded = true;
            }
        } 

        if(!notIncluded && totalCorrect == currentCase.Count && currentCase.Count == userThreads.Count / 2) 
        {
            correctAnswer = true;
        }
        Debug.Log($"Correct Answer: {correctAnswer}");
        return correctAnswer;

    }

    //Eventually I want to read in all the items and correct solutions for the case using JSON
    private void ReadThreads()
    {

    }
}
