using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

public static class PlayerParty
{
    //TODO: More list for different cases
    //Total List; Has everyone listed
    //Alive/Target list: Lists who is alive/can be targeted
    //How to handle removal?
    
    public static int MaxPartySize = 4;
    public static GameObject[] Party = new GameObject[MaxPartySize];

    public static void AddPartyMember(GameObject InputP)
    {
        //Adds Member to first empty slot
        for (int i = 0; i < MaxPartySize; i++)
        {
            if (Party[i] == null)
            {
                Party[i] = InputP;
                return;
            }
        }
    }
    
    public static List<GameObject> GetLivingPartyMembers()
    {
        List<GameObject> AliveList = new List<GameObject>();
        foreach (GameObject G in Party)
        {
            if (G.GetComponent<PlayableCharacter>().isAlive())
            {
                AliveList.Add(G);
            }
        }
        return AliveList;
    }
    
    public static GameObject getPartyMember(int i)
    {
        return Party[i];
    }
    
    public static int getPartySize()
    {
        return MaxPartySize;
    }
    
    public static int getLivingPartySize()
    {
        int i = 0;
        foreach (GameObject G in Party)
        {
            if (G.GetComponent<PlayableCharacter>().isAlive())
            {
                i++;
            }
        }
        return i;
    }
    
    public static int getPartyIndex(GameObject C)
    {
        int i = 0;
        foreach (GameObject G in Party)
        {
            if (G == C)
            {
                return i;
            }
            i++;
        }
        return -1;
    }
    
    public static GameObject[] getParty()
    {
        return Party;
    }
    
    //A party is dead when Party List is empty (since characters are removed on death)
    public static bool IsPartyDead()
    {
        foreach (GameObject G in Party)
        {
            if (G.GetComponent<PlayableCharacter>().isAlive())
            {
                return false;
            }
        }
        return true;
    }
}
