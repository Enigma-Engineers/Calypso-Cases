using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseData : MonoBehaviour
{
    private List<string> caseOne = new List<string>();
    private List<string> userThreads = new List<string>();
    [SerializeField] private Threads threads;
    // Start is called before the first frame update
    void Start()
    {
        caseOne.Add("Glass-Water");
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
