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
    
    public static int MaxEncounterSize;
    public static GameObject[] Encounter;
    public static EnemyEncounterData EE_Data;
    
    public static void LoadEncounter()
    {
        Debug.Log("Loading Encounter");
        EE_Data = GameObject.Find("EnemyEncounterDataGameObject").GetComponent<EnemyEncounterData>();
        MaxEncounterSize = EE_Data.EncounterSize;
        Encounter = new GameObject[MaxEncounterSize];
        for (int i = 0; i < MaxEncounterSize; i++)
        {
            GameObject E = EE_Data.InitialSpawnPool[i];
            if (E != null)
                Encounter[i] = EE_Data.CreateEnemy(i,i, true);
        }
    }
    
    
    public static List<GameObject> GetLivingEncounterMembers()
    {
        List<GameObject> AliveList = new List<GameObject>();
        foreach (GameObject G in Encounter)
        {
            if ((G != null) && G.GetComponent<EnemyCharacter>().isAlive())
            {
                AliveList.Add(G);
            }
        }
        return AliveList;
    }
    
    public static int GetLivingEncounterSize()
    {
        int i = 0;
        foreach (GameObject G in Encounter)
        {
            if ((G != null) && (G.GetComponent<PlayableCharacter>().isAlive()))
            {
                i++;
            }
        }
        return i;
    }
    
    //Will probably need to rework
    public static void ReplaceEncounterMember(int RespawnPool, int RespawnLocation)
    {
        if (Encounter[RespawnLocation] != null)
        {
            //Destroy the original value
            Object.Destroy(Encounter[RespawnLocation]);     
        }
        Encounter[RespawnLocation] = EE_Data.CreateEnemy(RespawnPool,RespawnLocation, false);
        
        HealthArmorHandler HA_Handler = GameObject.Find("HealthArmorHandlerGameObject").GetComponent<HealthArmorHandler>();
        HA_Handler.AddHealthIndicator(Encounter[RespawnLocation]);
        
        
    }
    
    public static GameObject getEncounterMember(int i)
    {
        return Encounter[i];
    }
    
    public static void setEncounterMember(int i, GameObject newEnemy)
    {
        if (i >= 0 && i < Encounter.Length)
        {
            Encounter[i] = newEnemy;
        }
        else
        {
            Debug.LogError($"Invalid position {i} for Encounter array.");
        }
    }


    public static int getEncounterIndex(GameObject C)
    {
        int i = 0;
        foreach (GameObject G in Encounter)
        {
            if (G == C)
            {
                return i;
            }
            i++;
        }
        return -1;
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
            if (G == null)
            {
                continue;
            }
            
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            if ((E.isAlive() || E.MultipleLives))
            {
                return false;
            }
        }
        return true;
    }
}
