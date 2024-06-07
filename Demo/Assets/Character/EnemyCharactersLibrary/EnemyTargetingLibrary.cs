using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using CharacterUtil;
using Random=UnityEngine.Random;
using BuffUtil;
using System.Linq;

namespace EnemyTargetingLibraryUtil
{
    
public static class EnemyTargetingLibrary
{
    public static Character[] TargetNRandomHeroes(int inputN)
    {
        int N = Mathf.Min(inputN, PlayerParty.getLivingPartySize());
        List<Character> randomList = new List<Character>();
        Character[] Targets = new Character[N];
        foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
        {
            randomList.Add((Character) G.GetComponent<PlayableCharacter>());
        }
        int i = 0;
        while (randomList.Count > 0 && i < N)
        {
            int r = Random.Range(0,randomList.Count);
            Character T = randomList[r];
            Targets[i] = T;
            randomList.Remove(T);
            i++;
        }
        
        Array.Sort(Targets, EnemyTargetingLibrary.ComparePlayerIndex);
        return Targets;
    }
    
    public static Character[] TargetNRandomEnemies(int inputN)
    {
        int N = Mathf.Min(inputN, EnemyEncounter.GetLivingEncounterSize());
        
        List<Character> randomList = new List<Character>();
        Character[] Targets = new Character[N];
        foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
        {
            randomList.Add((Character) G.GetComponent<EnemyCharacter>());
        }
        int i = 0;
        while (randomList.Count > 0 && i < N)
        {
            int r = Random.Range(0,randomList.Count);
            Character T = randomList[r];
            Targets[i] = T;
            randomList.Remove(T);
            i++;
        }
        
        Array.Sort(Targets, EnemyTargetingLibrary.CompareEnemyIndex);
        return Targets;
    }
    
    public static Character[] TargetEnemyType<T>()
    {
        List<Character> EnemyTypeList = new List<Character>();
        foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            if (typeof(T) == E.GetType())
            {
                EnemyTypeList.Add((Character) E);
            }
        }
        
        Character[] Targets = new Character[EnemyTypeList.Count];
        int i = 0;
        foreach (Character C in EnemyTypeList)
        {
            Targets[i] = C;
            i++;
        }
        
        return Targets;
        
    }
    
    //Target only characters that don't have a buff
    public static Character[] TargetNRandomHeroesBasedOnBuff(int inputN, Buff B, bool HasBuff, bool ExactBuff)
    {
        //Get a list of living party members
        List<Character> randomList = new List<Character>();
        foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
        {
            randomList.Add((Character) G.GetComponent<PlayableCharacter>());
                Debug.Log(G);
        }
        
        IEnumerable<Character> FilteredEnumerable = randomList.Where(C => ( HasBuff == BuffHandler.CharacterHaveBuff(C, B, ExactBuff) ) );
            Debug.Log(FilteredEnumerable.ToString());
         
        List<Character> FilteredList = FilteredEnumerable.ToList();
            Debug.Log(FilteredList.ToString());
         
        int N = Mathf.Min(inputN, FilteredList.Count);
            Debug.Log(N);
            Debug.Log(inputN);
        //Right now if the inputN is greater than the amount of buff targets
        //the enemy will activelly target less units
        //Should this be chagned such that extra targets are simply forced chosen from the non-valid pool?
        //If so, this needs to be reworked
         
        //List is empty
        if (N == 0)
        {
            //Chose a random target of inputN
            return TargetNRandomHeroes(1);
        }
        else
        {
            Character[] Targets = new Character[N];
            int i = 0;
            while (randomList.Count > 0 && i < N)
            {
                int r = Random.Range(0,FilteredList.Count);
                Character T = FilteredList[r];
                Targets[i] = T;
                FilteredList.Remove(T);
                i++;
            }
            return Targets;
        }
        
        
        
    }
    
    public static int[] CreateEvenDistributionToN(int N)
    {
        int[] IntArray = new int[N];
        
        List<int> randomList = new List<int>();
        for (int j = 0; j < N; j++)
        {
            randomList.Add(j);
        }
        
        int i = 0;
        while (randomList.Count > 0 && i < N)
        {
            int r = Random.Range(0, randomList.Count);
            int v = randomList[r];
            IntArray[i] = v;
            randomList.Remove(v);
            i++;
        }
        
        return IntArray;
    }
    
    public static int ComparePlayerIndex(Character C1, Character C2)
    {
        if (PlayerParty.getPartyIndex(C1.gameObject) > PlayerParty.getPartyIndex(C2.gameObject))
        {
            return 1;
        }
        return -1;
    }
    
    public static int CompareEnemyIndex(Character C1, Character C2)
    {
        if (EnemyEncounter.getEncounterIndex(C1.gameObject) > PlayerParty.getPartyIndex(C2.gameObject))
        {
            return 1;
        }
        return -1;
    }
}

}