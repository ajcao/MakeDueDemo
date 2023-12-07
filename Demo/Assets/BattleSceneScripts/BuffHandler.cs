using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using BuffUtil;
using EnemyMoveUtil;

public static class BuffHandler
{
    //Represents all
	//Hashtable mapping TriggerEventEnum -> Buff Dynamic Array
	private static Dictionary<TriggerEventEnum, List<Buff>> BuffsList;
	
	private static Queue<(TriggerEventEnum, TriggerEvent)> currentBuffProcList;
	
	private static Queue<(TriggerEventEnum, TriggerEvent)> TotalProcList;
	
	private static Queue<onDeathTrigger> HighPriorityProcList;
	
	private static Queue<(Character, Buff)> DelayedAddBuffList;
	
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
		DelayedAddBuffList = new Queue<(Character, Buff)>();
        
    }
	
	public static void AddBuff(Buff B, Character C)
	{
		//First checks if Trigger is already in process
		//if so, instead of adding buff now, add to a list for processing
		if (inBuffTriggerProcess)
		{
			DelayedAddBuffList.Enqueue((C, B));
			return;
		}
		C.getBuffList().Add(B);
		BuffsList[B.getTrigger()].Add(B);
		BuffsList[B.getTriggerSecondary()].Add(B);
	}
	
	public static bool CharacterHaveBuff(Character C, Buff InputB, bool Exact)
	{
		List<Buff> BuffList = C.getBuffList();
		
		foreach (Buff B in BuffList)
		{
			//Ensure the bufftype is the same
			if (InputB.GetType() == B.GetType())
			{
				//if Exact is true, then the intensity and duration must match
				if (Exact && (InputB.getIntensity() == B.getIntensity()) && (InputB.getDuration() == B.getDuration()))
					return true;
				
				//if Exact is false, then as long as the buff type matches return true
				if (!Exact)
					return true;
			}
		}
		
		return false;
	}
	public static void DecrementBuffDuration()
	{		
        foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
		{
			List<Buff> BList = G.GetComponent<Character>().getBuffList();

			int BuffCount = BList.Count;
			for (int i = 0; i < BuffCount; i++)
			{
				BList[i].decrementDuration();
			}
			BuffHandler.RemoveDeletedBuffsFromList(BList);
		}
		
        foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
		{
			List<Buff> BList = G.GetComponent<Character>().getBuffList();
			int BuffCount = BList.Count;
			for (int i = 0; i < BuffCount; i++)
			{
				BList[i].decrementDuration();
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
				GameObject.Destroy(B.BuffIndicator);
				B.BuffIndicator = null;
				BList.Remove(B);
				BuffsList[B.getTrigger()].Remove(B);
				BuffsList[B.getTriggerSecondary()].Remove(B);
				BuffHandler.RemoveDeletedBuffsFromList(BList);
				return;
			}
		}
 	}
	
	public static void MarkBuffsOnDeadCharacter(Character C)
	{
		List<Buff> BList = C.getBuffList();
		foreach (Buff B in BList)
		{
			B.PrepareBuffForDeletion();
		}
		
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
		
		//First checks if Trigger is already in process
		//if so, instead of triggering now,
		//add buff trigger to stack
		if (inBuffTriggerProcess)
		{
			AddTriggerToTotalProc(e, TE);
			return;
		}
		
		//Otherwise, proc the trigger instantly
		//Method is now in trigger process
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
					//Mark character as dead
					BattleLogicHandler.CharacterDies(DT.DyingCharacter);
					
					//Proc any on death buff effects
					//Phase Transitions also happens in buff effects here
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
					
					//Rewrite how death is handled
					//BattleLogicHandler should handle calling trigger of buffs
					//BuffHandler should really only be a storage. It should not determine when to trigger buffs
					
					//Death is handled here
					//Sphagetti Code
					
					//Mark all buffs on the character
					BuffHandler.MarkBuffsOnDeadCharacter(DT.DyingCharacter);
					
					//Move Characters offscreen
					if ((DT.DyingCharacter.GetType()).IsSubclassOf(typeof(EnemyCharacter)))
					{
						EnemyCharacter E = (EnemyCharacter) DT.DyingCharacter;
						if (!E.MultiplePhase)
							DT.DyingCharacter.gameObject.transform.position = new Vector3(0, -500, 0);
						else
						{
							//Delete movepool of Enemy while it waits to go to next phase
							Stack<EnemyMove> Moves = E.getCurrentMoves();
							while (Moves.Count > 0)
							{
								EnemyMove EM = E.getCurrentMoves().Pop();
								EM.DeleteMoveIndicator();
							}
							E.PrepareNextPhase();
						}
					}
					else
					{
						DT.DyingCharacter.gameObject.transform.position = new Vector3(0, -500, 0);
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
		
		//Remove all buffs
		BuffHandler.RemoveDeletedBuffsFromEveryone();
		
		//End trigger process
		inBuffTriggerProcess = false;
		
		//Add Delayed buffs
		while (DelayedAddBuffList.Count > 0)
		{
			(Character C, Buff B) = DelayedAddBuffList.Dequeue();
			C.getBuffList().Add(B);
			BuffsList[B.getTrigger()].Add(B);
			BuffsList[B.getTriggerSecondary()].Add(B);
		}
		
		
	}
}
