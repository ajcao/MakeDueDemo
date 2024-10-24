using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using BuffUtil;
using UnityEngine.Windows;

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
		
		TriggerEvent TE = new onDealDamageAddTrigger(AC, RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealDamageAddEnum, TE, ref d);
		
		TE = new onDealDamageMultiTrigger(AC, RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealDamageMultiEnum, TE, ref d);
		
		TE = new onDealDamageSpecialTrigger(RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealDamageSpecialEnum, TE, ref d);
		
		int damageToArmor = Mathf.Min(d, RC.getCurrentArmor());
		int damageToHealth = Mathf.Max(0, d - RC.getCurrentArmor());
		
		BattleLogicHandler.LowerArmor(RC, damageToArmor);
		
		
		//Displays damage
		GameObject.Find("DamageNumberHandler").GetComponent<DamageNumberHandler>().CreateDamageNumber(RC, damageToHealth);
		RC.setCurrentHealth(Mathf.Max(0, RC.getCurrentHealth() - damageToHealth));
		if (RC.getCurrentHealth() <= 0)
		{
			//Adds death trigger, to be processed after buffs proc
			TE = new onDeathTrigger(RC);
			BuffHandler.AddTriggerToHigherPrioirty((onDeathTrigger) TE);
		}
		
		TE = new onDealAttackDamagePostTrigger(AC, RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealAttackDamagePostEnum, TE, ref damageToArmor);
		
		TE = new onDealArmorDamagePostTrigger(AC, RC, damageToArmor);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealArmorDamagePostEnum, TE, ref damageToArmor);
		
		TE = new onDealHealthDamagePostTrigger(AC, RC, damageToHealth);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealHealthDamagePostEnum, TE, ref damageToHealth);
		
		if (damageToHealth > 0)
		{
			int dummy = 0;
			TE = new onHealthDamageWasTakenTrigger(RC, damageToHealth);
			BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onHealthDamageWasTakenEnum, TE, ref dummy);
		}
		
		if ((RC.GetType()).IsSubclassOf(typeof(PlayableCharacter)))
		{
			//Players gain resolve
			BattleLogicHandler.GainResolve((PlayableCharacter) RC, damageToArmor);
		}
		else
		{
			//Enemy lose armor
			BattleLogicHandler.LowerPoise((EnemyCharacter) RC, damageToHealth);
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
			BattleLogicHandler.LowerPoise((EnemyCharacter) RC, damageToHealth);
		}
		
		
		//Displays damage
		GameObject.Find("DamageNumberHandler").GetComponent<DamageNumberHandler>().CreateDamageNumber(RC, damageToHealth);
		RC.setCurrentHealth(Mathf.Max(0, RC.getCurrentHealth() - damageToHealth));
		if (RC.getCurrentHealth() <= 0)
		{
			//Adds death trigger, to be processed after buffs proc
			TE = new onDeathTrigger(RC);
			BuffHandler.AddTriggerToHigherPrioirty((onDeathTrigger) TE);
		}
		
		if (damageToHealth > 0)
		{
			int dummy = 0;
			TE = new onHealthDamageWasTakenTrigger(RC, damageToHealth);
			BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onHealthDamageWasTakenEnum, TE, ref dummy);
		}
	}
	
	public static void DirectlyLoseHP(Character C, int inputD)
	{
		//Displays damage
		GameObject.Find("DamageNumberHandler").GetComponent<DamageNumberHandler>().CreateDamageNumber(C, inputD);
		C.setCurrentHealth(Mathf.Max(0, C.getCurrentHealth() - inputD));
		TriggerEvent TE;
		
		if (C.getCurrentHealth() <= 0)
		{
			//Adds death trigger, to be processed after buffs proc
			TE = new onDeathTrigger(C);
			BuffHandler.AddTriggerToHigherPrioirty((onDeathTrigger) TE);
		}
		
		int dummy = 0;
		TE = new onHealthDamageWasTakenTrigger(C, inputD);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onHealthDamageWasTakenEnum, TE, ref dummy);
	}
	
	public static void BuffGainArmor(Character C, int inputD)
	{
		int d = inputD;
		
		C.setCurrentArmor(C.getCurrentArmor() + d);
		
		TriggerEvent TE = new onArmorWasGainedTrigger(C, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onArmorWasGainedEnum, TE, ref d);

		
	}
	
	public static void GainArmor(Character CC, Character RC, int inputD)
	{
		int d = inputD;
		
		TriggerEvent TE = new onArmorGainAddTrigger(CC, RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onArmorGainAddEnum, TE, ref d);
		
		TE = new onArmorGainMultiTrigger(CC, RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onArmorGainMultiEnum, TE, ref d);
		
		RC.setCurrentArmor(RC.getCurrentArmor() + d);
		
		TE = new onArmorGainPostTrigger(RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onArmorGainPostEnum, TE, ref d);
		
		TE = new onArmorWasGainedTrigger(RC, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onArmorWasGainedEnum, TE, ref d);
		
	}
	
	public static void LowerArmor(Character C, int d)
	{
		C.setCurrentArmor(Mathf.Max(C.getCurrentArmor() - d, 0));
	}
	
	public static void LowerPoise(EnemyCharacter E, int inputD)
	{
        int d = inputD;
		TriggerEvent TE;

        TE = new onDealPoiseDamageSpecialTrigger(E, d);
        BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onDealPoiseDamageSpecialEnum, TE, ref d);

        E.setPoise(Mathf.Max(E.getPoise() - d, 0));
		
		if (d > 0)
		{
			int dummy = 0;
			TE = new onPoiseWasLostTrigger(E, d);
			BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPoiseWasLostEnum, TE, ref dummy);
		}
		
	}
	
	public static void GainHealth(Character C, int inputD)
	{
		int d = inputD;
		
		TriggerEvent TE = new onHealthGainSpecialTrigger(C, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onHealthGainSpecialEnum, TE, ref d);
		
		C.setCurrentHealth(Mathf.Min(C.getCurrentHealth() + d, C.getMaxHealth()));
		
		TE = new onHealthGainPostTrigger(C, d);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onHealthGainPostEnum, TE, ref d);
	}
	
	public static void GainResolve(PlayableCharacter P, int inputR)
	{
		int r = inputR;
		
		TriggerEvent TE = new onResolveGainSpecialTrigger(P, r);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onResolveGainSpecialEnum, TE, ref r);
		
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
	
	public static void ActiveResolve(PlayableCharacter P)
	{
		TriggerEvent TE = new onPlayerActivateResolveTrigger(P);
		int dummy = 0;
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPlayerActivateResolveEnum, TE, ref dummy);
		
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
	//This function only marks character for death
	public static void CharacterDies(Character C)
	{
		//Move Characters offscreen
		if ((C.GetType()).IsSubclassOf(typeof(EnemyCharacter)))
		{
			EnemyCharacter E = (EnemyCharacter) C;
			E.DeleteMoves();
			
			if (!E.MultipleLives)
			{
				C.gameObject.transform.position = new Vector3(0, -500, 0);
			}
			else
			{
				//Keep Character but untag them so they cannot be targetted anymore
				C.gameObject.tag = "Untargetable";
			}
		}
		else
		{
			C.gameObject.transform.position = new Vector3(0, -500, 0);
		}
					
		//Destroy health/armor indicator
		UnityEngine.Object.Destroy(C.gameObject.GetComponentInChildren<HealthArmorScript>().gameObject);
		C.onDeath();
	}
	
	public static void CheckForEncounterDeath()
	{
		if (EnemyEncounter.IsEncounterDead())
		{
			BattleSceneHandler.EndGame?.Invoke();
		}
	}
	
	public static void CheckForAllPlayersDeaths()
	{
		if (PlayerParty.IsPartyDead())
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
	
	public static void PlayerPostTurn()
	{
		int dummy = 0;
		TriggerEvent TE = new onPostTurnTrigger(typeof(PlayableCharacter));
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPostTurnEnum, TE, ref dummy);
		
		TE = new onSecondPostTurnTrigger(typeof(PlayableCharacter));
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onSecondPostTurnEnum, TE, ref dummy);
		
	}
	

	public static void EnemyPreTurn()
	{
		foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
		{
			EnemyCharacter E = G.GetComponent<EnemyCharacter>();
			E.setCurrentArmor(Mathf.Min(E.getCurrentArmor(), E.getArmorRetain()));
		}
		int dummy = 0;
		TriggerEvent TE = new onPreTurnTrigger(typeof(EnemyCharacter));
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPreTurnEnum, TE, ref dummy);
	}
	
	public static void EnemyPostTurn()
	{
		int dummy = 0;
		TriggerEvent TE = new onPostTurnTrigger(typeof(EnemyCharacter));
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onPostTurnEnum, TE, ref dummy);
		
		TE = new onSecondPostTurnTrigger(typeof(EnemyCharacter));
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onSecondPostTurnEnum, TE, ref dummy);
		
	}
	
	//Decrease buff duration
	//Reset Poise
	//Reset Resolve
	//Force respawn of multiple phases enemies
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
            
            if (E.canPoiseRegenerate == true)
            {
                E.setPoise(Mathf.Min(E.getPoise() + E.getPoiseRegeneration(), E.getMaxPoise()));
            }
            
            if (E.IsStunned == true && !BuffHandler.CharacterHaveBuff(E, new StunnedBuff(null, null, null, 1), false))
            {
                E.IsStunned = false;
                E.setPoise(E.getMaxPoise());
				E.canPoiseRegenerate = true;
            }
			else if (E.IsStunned == true && BuffHandler.CharacterHaveBuff(E, new StunnedBuff(null, null, null, 1), false))
			{
				E.canPoiseRegenerate = false;
			}
			else
			{
				E.canPoiseRegenerate = true;
			}
            
        }
		
		int dummy = 0;
		TriggerEvent TE = new onRoundEndTrigger(R);
		BuffHandler.TriggerBuffsinBuffsList(TriggerEventEnum.onRoundEndEnum, TE, ref dummy);
	}
	
}
