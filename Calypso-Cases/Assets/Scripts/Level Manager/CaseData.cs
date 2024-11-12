using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseData : MonoBehaviour
{
    private List<string> caseOne = new List<string>();
    private List<string> caseTwo = new List<string>();
    private List<string> caseTwoOptional = new List<string>();
    private List<string> caseThree = new List<string>();

    private List<string> userThreads = new List<string>();
    [SerializeField] private Threads threads;
    // Start is called before the first frame update
    void Start()
    {
        caseOne.Add("Glass-Water");

        caseTwo.Add("Brass Knuckles-Hand Injury");
        caseTwo.Add("Scratch Marks (Wall)-Scratch Marks (Body)");
        caseTwo.Add("Scorch Marks (Wall)-Scorch Marks (Body)");

        caseTwoOptional.Add("Scratch Marks (Wall)-Bodyguard's Testimony");
        caseTwoOptional.Add("Scratch Marks (Body)-Bodyguard's Testimony");
        caseTwoOptional.Add("Scorch Marks (Wall)-Bartender's Testimony");
        caseTwoOptional.Add("Scorch Mark (Body)-Bartender's Testimony");

        caseThree.Add("CEO's Testimony-Painting");
        caseThree.Add("CEO's Testimony-Empty Box");
        caseThree.Add("Glass Shards-Empty Box");
        caseThree.Add("Footprints-Painting");
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

        foreach(string keyThread in caseOne) {
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

        if(!notIncluded && totalCorrect == caseOne.Count && caseOne.Count == userThreads.Count / 2) 
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
