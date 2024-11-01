using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    private bool hasWon = false;
    [SerializeField] private CaseData caseData;
    [SerializeField] private GameObject winCanvas;

    // Update is called once per frame
    void Update()
    {
        if (hasWon == false)
        {
            hasWon = caseData.Compare();
        }
        else
        {
            winCanvas.SetActive(true);
        }
    }
}
