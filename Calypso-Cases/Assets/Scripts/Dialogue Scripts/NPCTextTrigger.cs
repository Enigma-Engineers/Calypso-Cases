using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextTrigger : MonoBehaviour
{
    [Header("NPC Ink Text")]
    [SerializeField]
    private TextAsset defaultInkJSON;

    [SerializeField] private TextAsset magicInkJSON;

    public TextAsset DefaultInkJSON
    {
        get { return defaultInkJSON; }
    }

    public TextAsset MagicInkJSON
    {
        get { return magicInkJSON; }
    }
}
