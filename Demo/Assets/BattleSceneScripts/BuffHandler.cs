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
	
	private static Queue<(TriggerEventEnum, TriggerEvent)> currentBuffProcList;
	
	private static Queue<(TriggerEventEnum, TriggerEvent)> TotalProcList;
	
	private static Queue<onDeathTrigger> HighPriorityProcList;
	
	public static bool inBuffTriggerProcess = false;
    
    public static void Init()
    {
        //Init BuffsList
		BuffsList = new Dictionary<TriggerEventEnum, List<Buff>>();
        
        foreach (TriggerEventEnum e in Enum.GetValues(typeof(TriggerEventEnum)))
        {
            BuffsList[e] = new List<Buff>();
        }
		
		currentBuffProcList = new Queue<(TriggerEventEnum, TriggerEvent)>();
		TotalProcList = new Queue<(TriggerEventEnum, TriggerEvent)>();	
		HighPriorityProcList = new Queue<onDeathTrigger>();
        
    }
	
	public static void AddBuff(Buff B, Character C)
	{
		C.getBuffList().Add(B);
		BuffsList[B.getTrigger()].Add(B);
		BuffsList[B.getTriggerSecondary()].Add(B);
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
	
	private static void RemoveDeletedBuffsFromList(List<Buff> BList)
 	{
		for (int i = 0; i < BList.Count; i++)
 		{
			Buff B = BList[i];
			if (B.ToBeDeleted)
			{
				BList.Remove(B);
				BuffsList[B.getTrigger()].Remove(B);
				BuffsList[B.getTriggerSecondary()].Remove(B);
				BuffHandler.RemoveDeletedBuffsFromList(BList);
				return;
			}
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
	
	public static void RemoveDeletedBuffsFromEveryone()
	{
		foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
		{
			BuffHandler.RemoveDeletedBuffsFromList(G.GetComponent<Character>().getBuffList());
		}
		
		foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
		{
			BuffHandler.RemoveDeletedBuffsFromList(G.GetComponent<Character>().getBuffList());
		}
	}
	
	public static void AddTriggerToHigherPrioirty(onDeathTrigger TE)
	{
		HighPriorityProcList.Enqueue(TE);
	}
	
	public static void AddTriggerToTotalProc(TriggerEventEnum e, TriggerEvent TE)
	{
		TotalProcList.Enqueue((e, TE));
	}

	public static bool isTriggerWaiting()
	{
		return ( (TotalProcList.Count > 0) | (HighPriorityProcList.Count > 0) );
	}

	
	public static void TriggerBuffsinBuffsList(TriggerEventEnum e, TriggerEvent TE, ref int v)
	{
		inBuffTriggerProcess = true;
		
		if (BuffsList[e] != null)
		{
			//Only process currents buffs. Any more buffs added are not processed
			int BuffCount = BuffsList[e].Count;
			for (int i = 0; i < BuffCount; i++)
			{
				if (!BuffsList[e][i].ToBeDeleted)
				{
					BuffsList[e][i].onTriggerEffect(TE, ref v);
				}
			}
		}
		
		//While some buffs are still in the proc list
		while (BuffHandler.isTriggerWaiting())
		{
			//Move all triggers in waiting to the current trigger list
			while (TotalProcList.Count > 0)
			{
				currentBuffProcList.Enqueue(TotalProcList.Dequeue());
			}
			
			//Proc high priority triggrs for deaths
			//Check for actual death in case a dud shows up
			while (HighPriorityProcList.Count > 0)
			{
				onDeathTrigger DT = HighPriorityProcList.Dequeue();
				//Determine if character is truely dead
				//if so proc death trigger
				if (DT.DyingCharacter.getCurrentHealth() <= 0)
				{
					BattleLogicHandler.CharacterDies(DT.DyingCharacter);
					if (BuffsList[TriggerEventEnum.onDeathEnum] != null)
					{
						int BuffCount = BuffsList[TriggerEventEnum.onDeathEnum].Count;
						int dummy = 0;
						for (int i = 0; i < BuffCount; i++)
						{
							if (!BuffsList[TriggerEventEnum.onDeathEnum][i].ToBeDeleted)
							{
								BuffsList[TriggerEventEnum.onDeathEnum][i].onTriggerEffect(DT, ref dummy);
							}
						}
					}
				}
			}
			
			//Proc normal buffs in waiting
			while (currentBuffProcList.Count > 0)
			{
				(TriggerEventEnum currentEnum, TriggerEvent currentTE) = currentBuffProcList.Dequeue();
				if (BuffsList[currentEnum] != null)
				{
					int BuffCount = BuffsList[currentEnum].Count;
					int dummy = 0;
					for (int i = 0; i < BuffCount; i++)
					{
						if (!BuffsList[currentEnum][i].ToBeDeleted)
						{
							BuffsList[currentEnum][i].onTriggerEffect(currentTE, ref dummy);
						}
					}
				}
			}
		}
		
		//Remove any buffs that have a counter
		BuffHandler.RemoveDeletedBuffsFromEveryone();
		
		inBuffTriggerProcess = false;
	}
}
