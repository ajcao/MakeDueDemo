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
	
	
	public static void Damage(Character C, int inputD)
	{
		TriggerEvent TE = new onDealDamageTrigger(C, inputD);
		int d = inputD;
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealDamageEnum, TE, ref d);
		
		int damageToArmor = Mathf.Min(d, C.getCurrentArmor());
		int damageToHealth = Mathf.Max(0, d - C.getCurrentArmor());
		
		BattleLogicHandler.LowerArmor(C, damageToArmor);
		if ((C.GetType()).IsSubclassOf(typeof(PlayableCharacter)))
		{
			PlayableCharacter P = (PlayableCharacter) C;
			P.setResolve(P.getResolve() + damageToHealth);
		}
		else
		{
			BattleLogicHandler.LowerStamina((EnemyCharacter) C, damageToHealth);
		}
		GameObject.Find("DamageNumberHandler").GetComponent<DamageNumberHandler>().CreateDamageNumber(C, damageToHealth);
		C.setCurrentHealth(Mathf.Max(C.getCurrentHealth() - damageToHealth, 0));
	}
	
	public static void Armor(Character C, int d)
	{
		C.setCurrentArmor(C.getCurrentArmor() + d);
	}
	
	public static void LowerArmor(Character C, int d)
	{
		C.setCurrentArmor(Mathf.Max(C.getCurrentArmor() - d, 0));
	}
	
	public static void LowerStamina(EnemyCharacter E, int d)
	{
		E.setStamina(Mathf.Max(E.getStamina() - d, 0));
	}
	
	public static void Restore(Character C, int r)
	{
		C.setCurrentHealth(Mathf.Min(C.getCurrentHealth() + r, C.getMaxHealth()));
	}
	
	public static void AbilityCast(PlayableCharacter P, Character C)
	{
		TriggerEvent TE = new onPlayerAbilityTrigger(P, C);
		int dummy = 0;
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerAbilityEnum, TE, ref dummy);
	}
	
	public static void PostAbilityCast(PlayableCharacter P, Character C)
	{
		TriggerEvent TE = new onPostPlayerAbilityTrigger(P, C);
		int dummy = 0;
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPostPlayerAbilityEnum, TE, ref dummy);
		
	}
	
	public static void EnemyAttack(EnemyCharacter E, PlayableCharacter P, int inputD)
	{
		int d = inputD;
		TriggerEvent TE = new onEnemyAttackTrigger(E, P, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onEnemyAttackEnum, TE, ref d);
		Damage(P,d);
		BattleLog.Push(TE);
	}
	
	public static void PlayerAttack(PlayableCharacter P, EnemyCharacter E, int inputD)
	{
		TriggerEvent TE = new onPlayerAttackTrigger(P,E,inputD);
		int d = inputD;
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerAttackEnum, TE, ref d);
		Damage(E,d);
		BattleLog.Push(TE);
	}
	
	public static void OnBuffApply(Buff B)
	{
		B.onApplicationWrapper();
	}
	
	public static void PlayerDefend(PlayableCharacter DefP, PlayableCharacter RecP, int d)
	{
		//Create new Trigger Event for this
		RecP.setCurrentArmor(RecP.getCurrentArmor() + d);
		DefP.setResolve(DefP.getResolve() + d);
		if (DefP != RecP)
		{
			RecP.setResolve(RecP.getResolve() + d);
		}
		
	}
	
	public static void CharacterDies(Character C)
	{
		TriggerEvent TE = new onDeathTrigger(C);
		BattleLog.Push(TE);
		int dummy = 0;
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDeathEnum, TE, ref dummy);
		
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
		int dummy = 0;
		TriggerEvent TE = new onTurnStartTrigger(t);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onTurnStartEnum, TE, ref dummy);
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
		foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
		{
			PlayableCharacter C = G.GetComponent<PlayableCharacter>();
			C.setCurrentArmor(Mathf.Min(C.getCurrentArmor(), C.getArmorRetain()));
		}
		
	}
	
}
