using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

public static class PlayerParty
{    
    public static int PartySize = 4;
    
    public static GameObject[] Party = new GameObject[PartySize];

    public static void setPartyMember(GameObject InputP, int i)
    {
        Party[i] = InputP;
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
        return PartySize;
    }
    
    public static GameObject[] getParty()
    {
        return Party;
    }
}
