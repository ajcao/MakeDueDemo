using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

public static class EnemyEncounter
{    
    //TODO: More list for different cases
    //Total List; Has everyone listed
    //Alive/Target list: Lists who is alive/can be targeted
    //How to handle removal?
    public static List<GameObject> Encounter = new List<GameObject>();
    
    public static void AddEncounterMember(GameObject InputE)
    {
        Encounter.Add(InputE);
    }
    
    public static void RemoveEncounterMember(GameObject InputE)
    {
        Encounter.Remove(InputE);
    }
    
    public static GameObject getEncounterMember(int i)
    {
        return Encounter[i];
    }
    
    public static int getEncounterSize()
    {
        return Encounter.Count;
    }
    
    public static List<GameObject> getEncounter()
    {
        return Encounter;
    }
    
    public static bool IsEncounterDead()
    {
        return (Encounter.Count == 0);
    }
}
