using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace TriggerEventUtil
{
	
	
public enum TriggerEventEnum
{
	onRoundStartEnum,
	onPreTurnEnum,
	onPostTurnEnum,
	onSecondPostTurnEnum,
	onRoundEndEnum,
	
	onPlayerBasicAttackEnum,
	onPlayerBasicDefendEnum,
	onPlayerAttackEnum,
	onPlayerSkillEnum,
	onPlayerDefendEnum,
	onPlayerAbilityEnum,
	onPlayerAbilityPostEnum,
	
	onPlayerActivateResolveEnum,
	
	onDealDamageAddEnum,
	onDealDamageMultiEnum,
	onDealDamageSpecialEnum,
	
	onHealthDamageWasTakenEnum,
	
	onDealAttackDamagePostEnum,
	onDealArmorDamagePostEnum,
	onDealHealthDamagePostEnum,
	
	onStaminaWasLostEnum,
	
	onArmorGainAddEnum,
	//onArmorGainMultiEnum,
	//onArmorGainSpecialEnum,
	onArmorGainPostEnum,
	
	onArmorWasGainedEnum,
	
	onHealthGainSpecialEnum,
	onHealthGainEnum,
	
	
	onEnemyAttackEnum,
	onEnemySkillEnum,
	
	onDeathEnum,
	noTriggerEnum
}

public abstract class TriggerEvent
{
}


public class onRoundStartTrigger : TriggerEvent
{
	public int Turn;
	
	public onRoundStartTrigger(int t)
	{
		Turn = t;
	}
}

public class onPreTurnTrigger : TriggerEvent
{
	public Type CharacterType;
	
	public onPreTurnTrigger(Type inputCT)
	{
		CharacterType = inputCT;
	}
}

public class onPostTurnTrigger : TriggerEvent
{
	public Type CharacterType;
	
	public onPostTurnTrigger(Type inputCT)
	{
		CharacterType = inputCT;
	}
}

public class onSecondPostTurnTrigger : TriggerEvent
{
	public Type CharacterType;
	
	public onSecondPostTurnTrigger(Type inputCT)
	{
		CharacterType = inputCT;
	}
}

public class onRoundEndTrigger : TriggerEvent
{
	public int Turn;
	
	public onRoundEndTrigger(int t)
	{
		Turn = t;
	}
}

public class onPlayerBasicAttackTrigger : TriggerEvent
{
	public PlayableCharacter AttackingPlayer;
	public EnemyCharacter ReceivingEnemy;
	
	public onPlayerBasicAttackTrigger(PlayableCharacter P, EnemyCharacter E)
	{
		AttackingPlayer = P;
		ReceivingEnemy = E;
	}
}

public class onPlayerAttackTrigger : TriggerEvent
{
	public PlayableCharacter AttackingPlayer;
	public EnemyCharacter ReceivingEnemy;
	
	public onPlayerAttackTrigger(PlayableCharacter P, EnemyCharacter E)
	{
		AttackingPlayer = P;
		ReceivingEnemy = E;
	}
}

public class onPlayerSkillTrigger : TriggerEvent
{
	public PlayableCharacter CastingPlayer;
	public Character ReceivingCharacter;
	
	public onPlayerSkillTrigger(PlayableCharacter P, Character C)
	{
		CastingPlayer = P;
		ReceivingCharacter = C;
	}
}

public class onPlayerDefendTrigger : TriggerEvent
{
	public PlayableCharacter CastingPlayer;
	public PlayableCharacter ReceivingPlayer;
	
	public onPlayerDefendTrigger(PlayableCharacter P, PlayableCharacter C)
	{
		CastingPlayer = P;
		ReceivingPlayer = C;
	}
}

public class onPlayerBasicDefendTrigger : TriggerEvent
{
	public PlayableCharacter CastingPlayer;
	public PlayableCharacter ReceivingPlayer;
	
	public onPlayerBasicDefendTrigger(PlayableCharacter P, PlayableCharacter C)
	{
		CastingPlayer = P;
		ReceivingPlayer = C;
	}
}

public class onPlayerAbilityTrigger : TriggerEvent
{
	public PlayableCharacter CastingPlayer;
	public Character ReceivingCharacter;
	
	public onPlayerAbilityTrigger(PlayableCharacter P, Character C)
	{
		CastingPlayer = P;
		ReceivingCharacter = C;
	}
}

public class onPlayerAbilityPostTrigger : TriggerEvent
{
	public PlayableCharacter CastingPlayer;
	public Character ReceivingCharacter;
	
	public onPlayerAbilityPostTrigger(PlayableCharacter P, Character C)
	{
		CastingPlayer = P;
		ReceivingCharacter = C;
	}
}

public class onPlayerActivateResolveTrigger : TriggerEvent
{
	public PlayableCharacter CastingPlayer;
	
	public onPlayerActivateResolveTrigger(PlayableCharacter P)
	{
		CastingPlayer = P;
	}
}

public class onDealDamageAddTrigger : TriggerEvent
{
	public Character AttackingChar;
	public Character ReceivingChar; 
	public int DamageAmount;
	
	public onDealDamageAddTrigger(Character AC, Character RC, int d)
	{
		AttackingChar = AC;
		ReceivingChar = RC;
		DamageAmount = d;
	}
}

public class onDealDamageMultiTrigger : TriggerEvent
{
	public Character ReceivingChar; 
	public int DamageAmount;
	
	public onDealDamageMultiTrigger(Character RC, int d)
	{
		ReceivingChar = RC;
		DamageAmount = d;
	}
}

public class onDealDamageSpecialTrigger : TriggerEvent
{
	public Character ReceivingChar; 
	public int DamageAmount;
	
	public onDealDamageSpecialTrigger(Character RC, int d)
	{
		ReceivingChar = RC;
		DamageAmount = d;
	}
}

public class onHealthDamageWasTakenTrigger : TriggerEvent
{
	public Character ReceivingChar;
	public int DamageAmount;
	
	public onHealthDamageWasTakenTrigger(Character RC, int a)
	{
		Debug.Log("waking up");
		ReceivingChar = RC;
		DamageAmount = a;
	}
	
}

public class onDealAttackDamagePostTrigger : TriggerEvent
{
	public Character AttackingChar;
	public Character ReceivingChar; 
	public int DamageAmount;
	
	public onDealAttackDamagePostTrigger(Character AC, Character RC, int d)
	{
		AttackingChar = AC;
		ReceivingChar = RC;
		DamageAmount = d;
	}
}


public class onDealArmorDamagePostTrigger : TriggerEvent
{
	public Character AttackingChar;
	public Character ReceivingChar; 
	public int ArmorAmount;
	
	public onDealArmorDamagePostTrigger(Character AC, Character RC, int d)
	{
		AttackingChar = AC;
		ReceivingChar = RC;
		ArmorAmount = d;
	}
}

public class onDealHealthDamagePostTrigger : TriggerEvent
{
	public Character AttackingChar;
	public Character ReceivingChar; 
	public int DamageAmount;
	
	public onDealHealthDamagePostTrigger(Character AC, Character RC, int d)
	{
		AttackingChar = AC;
		ReceivingChar = RC;
		DamageAmount = d;
	}
}

public class onStaminaWasLostTrigger : TriggerEvent
{
	public EnemyCharacter ReceivingChar;
	public int StaminaAmount;
	
	public onStaminaWasLostTrigger(EnemyCharacter RC, int a)
	{
		ReceivingChar = RC;
		StaminaAmount = a;
	}
	
}

public class onArmorGainAddTrigger : TriggerEvent
{
	public Character CastingChar;
	public Character ReceivingChar;
	public int ArmorAmount;
	
	public onArmorGainAddTrigger(Character CC, Character RC, int a)
	{
		CastingChar = CC;
		ReceivingChar = RC;
		ArmorAmount = a;
	}
	
}

public class onArmorGainPostTrigger : TriggerEvent
{
	public Character ReceivingChar;
	public int ArmorAmount;
	
	public onArmorGainPostTrigger(Character RC, int a)
	{
		ReceivingChar = RC;
		ArmorAmount = a;
	}
	
}

public class onArmorWasGainedTrigger : TriggerEvent
{
	public Character ReceivingChar;
	public int ArmorAmount;
	
	public onArmorWasGainedTrigger(Character RC, int a)
	{
		ReceivingChar = RC;
		ArmorAmount = a;
	}
	
}

public class onHealthGainSpecialTrigger : TriggerEvent
{
	public Character ReceivingChar; 
	public int HealthAmount;
	
	public onHealthGainSpecialTrigger(Character RC, int d)
	{
		ReceivingChar = RC;
		HealthAmount = d;
	}
}

public class onEnemyAttackTrigger : TriggerEvent
{
	public EnemyCharacter AttackingEnemy;
	public PlayableCharacter ReceivingPlayer; 
	
	public onEnemyAttackTrigger(EnemyCharacter E, PlayableCharacter P)
	{
		AttackingEnemy = E;
		ReceivingPlayer = P;
	}
}

public class onEnemySkillTrigger : TriggerEvent
{
	public EnemyCharacter CastingEnemy;
	public PlayableCharacter ReceivingCharacter; 
	
	public onEnemySkillTrigger(EnemyCharacter E, PlayableCharacter C)
	{
		CastingEnemy = E;
		ReceivingCharacter = C;
	}
}

public class onBuffExpireTrigger : TriggerEvent
{
	public Character BuffHolder;
	public Buff ExpiredBuff;
	
	public onBuffExpireTrigger(Character C, Buff B)
	{
		BuffHolder = C;
		ExpiredBuff = B;
	}
}


public class onDeathTrigger : TriggerEvent
{
	public Character DyingCharacter;
	
	public onDeathTrigger(Character C)
	{
		DyingCharacter = C;
	}
}
	
	
}


