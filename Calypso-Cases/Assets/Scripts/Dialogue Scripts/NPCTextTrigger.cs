using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextTrigger : MonoBehaviour
{
    [Header("NPC Ink Text")]
    [SerializeField]
    private TextAsset inkJSON;

    public TextAsset InkJSON
    {
        get { return inkJSON; }
    }
}
