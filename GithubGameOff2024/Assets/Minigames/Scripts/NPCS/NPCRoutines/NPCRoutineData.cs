using System.Collections.Generic;
using UnityEngine;

public struct Routine
{
    float time;
    string activity;
}

[System.Serializable]
[CreateAssetMenu(fileName = "NPC Routine", menuName = "Scriptables/NPC Routine", order = 1)]
public class NPCRoutineData : ScriptableObject
{
    public Routine[] routines; //Can expand to have 7 arrays for each day of the week
}
