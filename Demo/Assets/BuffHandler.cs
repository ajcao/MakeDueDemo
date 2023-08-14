using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using BuffUtil;

public static class BuffHandler
{
    //Represents all
	//Hashtable mapping TriggerEventEnum -> Buff Dynamic Array
	private static Dictionary<TriggerEventEnum, List<Buff>> BuffsList;
    
    public static void Init()
    {
        //Init BuffsList
		BuffsList = new Dictionary<TriggerEventEnum, List<Buff>>();
        
        foreach (TriggerEventEnum e in Enum.GetValues(typeof(TriggerEventEnum)))
        {
            BuffsList[e] = new List<Buff>();
        }
        
    }
	
	public static void AddBuff(Buff B, Character C)
	{
		C.getBuffList().Add(B);
		BuffsList[B.getTrigger()].Add(B);
	}
	
	public static void DecrementBuffDuration()
	{		
		foreach (GameObject G in PlayerParty.getParty())
		{
			List<Buff> BList = G.GetComponent<Character>().getBuffList();
			foreach (Buff B in BList)
			{
				B.decrementDuration();
			}
			BuffHandler.RemoveDeletedBuffsFromList(BList);
		}
		
		foreach (GameObject G in EnemyEncounter.getEncounter())
		{
			List<Buff> BList = G.GetComponent<Character>().getBuffList();
			foreach (Buff B in BList)
			{
				B.decrementDuration();
			}
			BuffHandler.RemoveDeletedBuffsFromList(BList);
		}
	}
	
	public static void RemoveBuffsFromDeadCharacter(Character C)
	{
		List<Buff> BList = C.getBuffList();
		foreach (Buff B in BList)
		{
			B.PrepareBuffForDeletion();
		}
		BuffHandler.RemoveDeletedBuffsFromList(BList);
		
	}
	
	private static void RemoveDeletedBuffsFromList(List<Buff> BList)
	{
		for (int i = 0; i < BList.Count; i++)
		{
			Buff B = BList[i];
			if (B.ToBeDeleted)
			{
				BList.Remove(B);
				BuffsList[B.getTrigger()].Remove(B);
				BuffHandler.RemoveDeletedBuffsFromList(BList);
				return;
			}
		}
	}
	
	public static void TriggerBuffsinBuffsList(TriggerEventEnum e, TriggerEvent TE)
	{
		if (BuffsList[e] != null)
		{
			foreach (Buff B in BuffsList[e])
			{
				B.onTriggerEffect(TE);
			}
		}
	}
}