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
	

	
	public static void Init()
	{
		//Init Battle Log
		BattleLog = new Stack<TriggerEvent>();
		
		//Init Buff Handler
		BuffHandler.Init();
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
		TriggerEvent TE = new onEnemyAttackTrigger(E, P, d);
		int armor = P.getCurrentArmor();
		P.GiveResolve(Mathf.Min(armor, d));
		P.setResolve(P.getResolve() + Mathf.Max(d-armor,0));
		Damage(P,d);
		BattleLog.Push(TE);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onEnemyAttackEnum, TE);
	}
	
	public static void PlayerAttack(PlayableCharacter P, EnemyCharacter E, int inputD)
	{
		TriggerEvent TE = new onPlayerAttackTrigger(P,E,inputD);
		int d;
		if (E.IsStunned)
		{
			d = inputD * 2;
		}
		else
		{
			d = inputD;
		}
		Damage(E,d);
		E.setStamina(Mathf.Max(E.getStamina() - d));
		BattleLog.Push(TE);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerAttackEnum, TE);
	}
	
	public static void OnBuffApply(Buff B)
	{
		B.onApplicationWrapper();
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
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDeathEnum, TE);
		
		BuffHandler.RemoveBuffsFromDeadCharacter(C);
		
		//Move character offscreen (should BattleLogicHandler handle this?)
		C.gameObject.transform.position = new Vector3(0, -500, 0);
		
		//Check for Player Party death. If so, end game immeditely
		//Game does not end for immeditely for Enemy Encounters since
		//helpful buffs should still trigger before game ends
		if (PlayerParty.IsPartyDead())
		{
			BattleSceneHandler.EndGame?.Invoke();
		}
	}
	
	public static void CheckForEncounterDeath()
	{
		if (EnemyEncounter.IsEncounterDead())
		{
			BattleSceneHandler.EndGame?.Invoke();
		}
	}
	
	public static void BeginRound(int t)
	{
		//Start the turn
		TriggerEvent TE = new onTurnStartTrigger(t);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onTurnStartEnum, TE);
	}
	public static void PlayerPreTurn()
	{
		foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
		{
			PlayableCharacter C = G.GetComponent<PlayableCharacter>();
			C.setCurrentArmor(Mathf.Min(C.getCurrentArmor(), C.getArmorRetain()));
		}
	}
	public static void EnemyPreTurn()
	{
		foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
		{
			EnemyCharacter C = G.GetComponent<EnemyCharacter>();
			C.setCurrentArmor(Mathf.Min(C.getCurrentArmor(), C.getArmorRetain()));
		}
	}
	
	//Decrease buff duration
	public static void EndCombatRound()
	{
		BuffHandler.DecrementBuffDuration();
		
	}
	
}
