using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

public static class EnemyEncounter
{    
    public static GameObject[] Encounter;
    
    public static void createNewEncounter(int i)
    {
        Encounter = new GameObject[i];
    }
    
    public static void setEncounterMember(GameObject InputE, int i)
    {
        Encounter[i] = InputE;
    }
    
    public static GameObject getEncounterMember(int i)
    {
        return Encounter[i];
    }
    
    public static int getEncounterSize()
    {
        return Encounter.Length;
    }
    
    public static GameObject[] getEncounter()
    {
        return Encounter;
    }
}
