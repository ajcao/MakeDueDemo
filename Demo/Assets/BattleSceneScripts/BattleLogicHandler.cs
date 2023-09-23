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
	
	
	public static void AttackDamage(Character AC, Character RC, int inputD)
	{
		int d = inputD;
		
		TriggerEvent TE = new onDealDamageAddTrigger(RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealDamageAddEnum, TE, ref d);
		
		TE = new onDealDamageMultiTrigger(RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealDamageMultiEnum, TE, ref d);
		
		TE = new onDealDamageSpecialTrigger(RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealDamageSpecialEnum, TE, ref d);
		
		int damageToArmor = Mathf.Min(d, RC.getCurrentArmor());
		int damageToHealth = Mathf.Max(0, d - RC.getCurrentArmor());
		
		BattleLogicHandler.LowerArmor(RC, damageToArmor);
		if ((RC.GetType()).IsSubclassOf(typeof(PlayableCharacter)))
		{
			BattleLogicHandler.GainResolve((PlayableCharacter) RC, damageToArmor);
		}
		else
		{
			BattleLogicHandler.LowerStamina((EnemyCharacter) RC, damageToHealth);
		}
		
		
		//Displays damage
		GameObject.Find("DamageNumberHandler").GetComponent<DamageNumberHandler>().CreateDamageNumber(RC, damageToHealth);
		RC.setCurrentHealth(RC.getCurrentHealth() - damageToHealth);
		if (RC.getCurrentHealth() <= 0)
		{
			//Adds death trigger, to be processed after buffs proc
			TE = new onDeathTrigger(RC);
			BuffHandler.AddTriggerToHigherPrioirty((onDeathTrigger) TE);
		}
		
		TE = new onDealAttackDamagePostTrigger(AC, RC, d);
		if (BuffHandler.inBuffTriggerProcess)
		{
			BuffHandler.AddTriggerToTotalProc(TriggerEventEnum.onDealAttackDamagePostEnum, TE);
		}
		else
		{
			BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealAttackDamagePostEnum, TE, ref d);
		}
	}
	
	public static void BuffDamage(Character RC, int inputD)
	{
		int d = inputD;
		TriggerEvent TE;
		
		int damageToArmor = Mathf.Min(d, RC.getCurrentArmor());
		int damageToHealth = Mathf.Max(0, d - RC.getCurrentArmor());
		
		BattleLogicHandler.LowerArmor(RC, damageToArmor);
		if ((RC.GetType()).IsSubclassOf(typeof(PlayableCharacter)))
		{
			BattleLogicHandler.GainResolve((PlayableCharacter) RC, damageToArmor);
		}
		else
		{
			BattleLogicHandler.LowerStamina((EnemyCharacter) RC, damageToHealth);
		}
		
		
		//Displays damage
		GameObject.Find("DamageNumberHandler").GetComponent<DamageNumberHandler>().CreateDamageNumber(RC, damageToHealth);
		RC.setCurrentHealth(RC.getCurrentHealth() - damageToHealth);
		if (RC.getCurrentHealth() <= 0)
		{
			//Adds death trigger, to be processed after buffs proc
			TE = new onDeathTrigger(RC);
			BuffHandler.AddTriggerToHigherPrioirty((onDeathTrigger) TE);
		}
	}
	
	public static void GainArmor(Character C, int d)
	{
		C.setCurrentArmor(C.getCurrentArmor() + d);
		TriggerEvent TE = new onArmorGainTrigger(C, d);
		if (BuffHandler.inBuffTriggerProcess)
		{
			BuffHandler.AddTriggerToTotalProc(TriggerEventEnum.onArmorGainEnum, TE);
		}
		else
		{
			BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onArmorGainEnum, TE, ref d);
		}
		
	}
	
	public static void LowerArmor(Character C, int d)
	{
		C.setCurrentArmor(Mathf.Max(C.getCurrentArmor() - d, 0));
	}
	
	public static void LowerStamina(EnemyCharacter E, int d)
	{
		E.setStamina(Mathf.Max(E.getStamina() - d, 0));
	}
	
	public static void GainHealth(Character C, int r)
	{
		C.setCurrentHealth(Mathf.Min(C.getCurrentHealth() + r, C.getMaxHealth()));
	}
	
	public static void GainResolve(PlayableCharacter P, int r)
	{
		P.setResolve(Mathf.Min(P.getResolve() + r, P.getMaxResolve()));
	}
	
	public static void AbilityCast(PlayableCharacter P, Character C)
	{
		TriggerEvent TE = new onPlayerAbilityTrigger(P, C);
		int dummy = 0;
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerAbilityEnum, TE, ref dummy);
	}
	
	public static void PostAbilityCast(PlayableCharacter P, Character C)
	{
		TriggerEvent TE = new onPlayerAbilityPostTrigger(P, C);
		int dummy = 0;
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerAbilityPostEnum, TE, ref dummy);
		
	}
	
	public static void PlayerBasicAttack(PlayableCharacter P, EnemyCharacter E)
	{
		int dummy = 0;
		
		TriggerEvent TE = new onPlayerBasicAttackTrigger(P,E);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerBasicAttackEnum, TE, ref dummy);
	}
	
	public static void PlayerAttack(PlayableCharacter P, EnemyCharacter E)
	{
		int dummy = 0;
		
		TriggerEvent TE = new onPlayerAttackTrigger(P,E);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerAttackEnum, TE, ref dummy);
	}
	
	public static void PlayerBasicDefend(PlayableCharacter P, PlayableCharacter E)
	{
		int dummy = 0;
		
		TriggerEvent TE = new onPlayerBasicDefendTrigger(P,E);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerBasicDefendEnum, TE, ref dummy);
	}
	
	public static void PlayerDefend(PlayableCharacter P, PlayableCharacter E)
	{
		int dummy = 0;
		
		TriggerEvent TE = new onPlayerDefendTrigger(P,E);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerDefendEnum, TE, ref dummy);
	}
	
	public static void PlayerSkill(PlayableCharacter P, Character E)
	{
		int dummy = 0;
		
		TriggerEvent TE = new onPlayerSkillTrigger(P,E);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerSkillEnum, TE, ref dummy);
	}
	
	public static void OnBuffApply(Buff B)
	{
		B.onApplicationWrapper();
	}
	
	//Trigger is handled by damage script and buff handler
	//Since death can only be safely determined after all buffs trigger
	public static void CharacterDies(Character C)
	{
		C.onDeath();
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
		//Start the beginning of the round
		int dummy = 0;
		TriggerEvent TE = new onRoundStartTrigger(t);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onRoundStartEnum, TE, ref dummy);
	}
	
	public static void PlayerPreTurn()
	{
		foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
		{
			PlayableCharacter P = G.GetComponent<PlayableCharacter>();
			P.setCurrentArmor(Mathf.Min(P.getCurrentArmor(), P.getArmorRetain()));
		}
		int dummy = 0;
		TriggerEvent TE = new onPreTurnTrigger(typeof(PlayableCharacter));
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPreTurnEnum, TE, ref dummy);
		
	}
	

	public static void EnemyPreTurn()
	{
		foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
		{
			EnemyCharacter E = G.GetComponent<EnemyCharacter>();
			E.setCurrentArmor(Mathf.Min(E.getCurrentArmor(), E.getArmorRetain()));
		}
		Character C = EnemyEncounter.GetLivingEncounterMembers()[0].GetComponent<EnemyCharacter>();;
		int dummy = 0;
		TriggerEvent TE = new onPreTurnTrigger(typeof(EnemyCharacter));
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPreTurnEnum, TE, ref dummy);
	}
	
	//Decrease buff duration
	//Reset Stamina
	//Reset Resolve
	public static void EndCombatRound(int R)
	{
		BuffHandler.DecrementBuffDuration();
		
        foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
        {
            G.GetComponent<PlayableCharacter>().RefreshCasting();
            G.GetComponent<PlayableCharacter>().FullHealthResolveBonus();
            
        }
		
		foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            
            if (E.canStaminaRegenerate == true)
            {
                E.setStamina(Mathf.Min(E.getStamina() + E.getStaminaRegeneration(), E.getMaxStamina()));
            }
            E.canStaminaRegenerate = true;
            
            if (E.IsStunned == true)
            {
                E.IsStunned = false;
                E.setStamina(E.getMaxStamina());
            }
            
        }
	}
	
}
