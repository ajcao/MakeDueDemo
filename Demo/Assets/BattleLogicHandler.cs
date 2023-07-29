using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using BuffUtil;

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
	
	public static Dictionary<TriggerEventEnum, List<Buff>> getBuffsList()
	{
		return BuffsList;
	}
	
	public static void Damage(Character C, int d)
	{
		int armor = C.getCurrentArmor();
		C.setCurrentArmor(Mathf.Max(armor - d, 0));
		C.setCurrentHealth(Mathf.Max(C.getCurrentHealth() - Mathf.Max(d - armor, 0), 0));
	}
	
	public static void Armor(Character C, int d)
	{
		C.setCurrentArmor(C.getCurrentArmor() + d);
	}
	
	public static void Restore(Character C, int r)
	{
		C.setCurrentHealth(Mathf.Min(C.getCurrentHealth() + r, C.getMaxHealth()));
	}
	
	public static void EnemyAttack(EnemyCharacter E, PlayableCharacter P, int d)
	{
		int armor = P.getCurrentArmor();
		P.GiveResolve(Mathf.Min(armor, d));
		P.setResolve(P.getResolve() + Mathf.Max(d-armor,0));
		Damage(P,d);
	}
	
	public static void PlayerAttack(PlayableCharacter P, EnemyCharacter E, int d)
	{
		TriggerEvent TE = new onPlayerAttackTrigger(P,E,d);
		Damage(E,d);
		E.setStamina(Mathf.Max(E.getStamina() - d));
		BattleLog.Push(TE);
		TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerAttackEnum, TE);
	}
	
	public static void OnBuffApply(Buff B)
	{
		B.onApplication();
	}
	
	public static void PlayerDefend(PlayableCharacter DefP, PlayableCharacter RecP, int d)
	{
		//Create new Trigger Event for this
		RecP.setCurrentArmor(RecP.getCurrentArmor() + d);
		RecP.setProtectionList(DefP);
	}
	
	public static void CharacterDies(Character C)
	{
		TriggerEvent TE = new onDeathTrigger(C);
		BattleLog.Push(TE);
		TriggerBuffsinBuffsList(TriggerEventEnum.onDeathEnum, TE);
	}
	
	public static void PlayerPreTurn()
	{
		foreach (GameObject G in PlayerParty.getParty())
		{
			PlayableCharacter C = G.GetComponent<PlayableCharacter>();
			C.setCurrentArmor(Mathf.Min(C.getCurrentArmor(), C.getArmorRetain()));
		}
	}
	public static void EnemyPreTurn()
	{
		foreach (GameObject G in EnemyEncounter.getEncounter())
		{
			EnemyCharacter C = G.GetComponent<EnemyCharacter>();
			C.setCurrentArmor(Mathf.Min(C.getCurrentArmor(), C.getArmorRetain()));
		}
	}
	
	private static void TriggerBuffsinBuffsList(TriggerEventEnum e, TriggerEvent TE)
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
