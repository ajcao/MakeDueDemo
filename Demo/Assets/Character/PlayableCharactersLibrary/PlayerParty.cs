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
    public static List<GameObject> Party =  new List<GameObject>();

    public static void AddPartyMember(GameObject InputP)
    {
        Party.Add(InputP);
    }
    
    public static void RemovePartyMember(GameObject InputP)
    {
        Party.Remove(InputP);
    }
    
    public static GameObject getPartyMember(int i)
    {
        return Party[i];
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
    
    public static int getPartySize()
    {
        return Party.Count;
    }
    
    public static List<GameObject> getParty()
    {
        return Party;
    }
    
    //A party is dead when Party List is empty (since characters are removed on death)
    public static bool IsPartyDead()
    {
        return (Party.Count == 0);
    }
}
