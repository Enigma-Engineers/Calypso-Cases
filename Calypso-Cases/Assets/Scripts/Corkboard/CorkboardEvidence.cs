using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorkboardEvidence : MonoBehaviour
{
    [SerializeField] public string itemName;

    private void Start()
    {

    }

    public string getName()
    {
        return itemName;
    }
}
