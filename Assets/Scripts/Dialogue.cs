using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue
{
    public string npcName;
    [TextArea(3, 10)] 
    public string[] sentence;
    public string[] choices;
    public int[] choiceMissionIDs;
}
