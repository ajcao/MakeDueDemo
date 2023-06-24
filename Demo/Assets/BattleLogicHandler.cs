using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

public static class BattleLogicHandler
{
	//Battle log of all TriggerEvents that has happened
	public static Stack<TriggerEvent> BattleLog;
	
	//Represents all
	//Hashtable mapping TriggerEventEnum -> Buff Dynamic Array
	private static Dictionary<TriggerEventEnum, List<Buff>> BuffsList;

	
	public static void Init()
	{
		//Init Battle Log
		BattleLog = new Stack<TriggerEvent>();
		
		//Init BuffsList
		BuffsList = new Dictionary<TriggerEventEnum, List<Buff>>();
		
		foreach (TriggerEventEnum e in Enum.GetValues(typeof(TriggerEventEnum)))
        {
            BuffsList[e] = new List<Buff>();
        }
	}
	
	public static void Damage(Character C, int d)
	{
		C.setCurrentHealth(Mathf.Max(C.getCurrentHealth() - d, 0));
	}
	
	public static void Restore(Character C, int r)
	{
		C.setCurrentHealth(Mathf.Min(C.getCurrentHealth() + r, C.getMaxHealth()));
	}
	
	public static void PlayerAttack(PlayableCharacter P, EnemyCharacter E, int d)
	{
		TriggerEvent TE = new onPlayerAttackTrigger(P,E,d);
		Damage(E,d);
		BattleLog.Push(TE);
		TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerAttackEnum, TE);
	}
	
	public static void PlayerDefend(PlayableCharacter DefP, PlayableCharacter RecP, int d)
	{
		//Create new Trigger Event for this
		RecP.setCurrentArmor(RecP.getCurrentArmor() + d);
	}
	
	public static void CharacterDies(Character C)
	{
		TriggerEvent TE = new onDeathTrigger(C);
		BattleLog.Push(TE);
		TriggerBuffsinBuffsList(TriggerEventEnum.onDeathEnum, TE);
	}
	
	private static void TriggerBuffsinBuffsList(TriggerEventEnum e, TriggerEvent TE)
	{
		if (BuffsList[e] != null)
		{
			foreach (Buff B in BuffsList[e])
			{
				B.onEffect(TE);
			}
		}
	}
}
