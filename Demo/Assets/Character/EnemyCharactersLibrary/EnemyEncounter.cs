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
    public static int MaxEncounterSize = 2;
    public static GameObject[] Encounter = new GameObject[MaxEncounterSize];
    
    public static void AddEncounterMember(GameObject InputE)
    {
        //Adds Enemy to first empty slot
        for (int i = 0; i < MaxEncounterSize; i++)
        {
            if (Encounter[i] == null)
            {
                Encounter[i] = InputE;
                return;
            }
        }
    }
    
    public static List<GameObject> GetLivingEncounterMembers()
    {
        List<GameObject> AliveList = new List<GameObject>();
        foreach (GameObject G in Encounter)
        {
            if (G.GetComponent<EnemyCharacter>().isAlive())
            {
                AliveList.Add(G);
            }
        }
        return AliveList;
    }
    
    //Will probably need to rework
    public static void ReplaceEncounterMember(GameObject InputE)
    {
        for (int i = 0; i < MaxEncounterSize; i++)
        {
            //Enemy is same type and dead)
            EnemyCharacter E = Encounter[i].GetComponent<EnemyCharacter>();
            if ((E.GetType() == InputE.GetType()) && !E.isAlive())
            {
                Encounter[i] = InputE;
            }
        }
    }
    
    public static GameObject getEncounterMember(int i)
    {
        return Encounter[i];
    }
    
    public static int getEncounterSize()
    {
        return MaxEncounterSize;
    }
    
    public static GameObject[] getEncounter()
    {
        return Encounter;
    }
    
    public static bool IsEncounterDead()
    {
        foreach (GameObject G in Encounter)
        {
            if (G.GetComponent<EnemyCharacter>().isAlive())
            {
                return false;
            }
        }
        return true;
    }
}
