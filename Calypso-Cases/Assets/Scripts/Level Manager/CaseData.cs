using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseData : MonoBehaviour
{
    private List<string> key = new List<string>();
    private List<string> userThreads = new List<string>();
    [SerializeField] private Threads threads;
    // Start is called before the first frame update
    void Start()
    {
        key.Add("Glass-Hammer");
    }

    private void Update()
    {
        userThreads = threads.getThreads();
        if(userThreads.Count > 1 && userThreads != null)
        {
            Compare();
        }
        
    }


    private bool Compare()
    {
        int totalCorrect = 0;
        int prevTotal = 0;
        bool hasExtra = false;
        bool correctAnswer = false;

        foreach(string keyThread in key) {
            prevTotal = totalCorrect;
            for (int i = 0; i < userThreads.Count; i++)
            {
                if(i % 2 == 0 && i + 1 < userThreads.Count)
                {
                    if (keyThread.Equals($"{userThreads[i]}-{userThreads[i + 1]}") || keyThread.Equals($"{userThreads[i + 1]}-{userThreads[i]}"))
                    {
                        totalCorrect++;
                    }
                }
            }
            if (totalCorrect == prevTotal) { 
                hasExtra = true;
            }
        } 

        if(!hasExtra && totalCorrect >= key.Count) 
        {
            correctAnswer = true;
        }
        return correctAnswer;

    }
    //Eventually I want to read in all the items and correct solutions for the case using JSON
    private void ReadThreads()
    {

    }
}
