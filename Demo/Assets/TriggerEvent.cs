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
	onRoundEndEnum,
	
	onPlayerAttackEnum,
	onPlayerSkillEnum,
	onPlayerAbilityEnum,
	onPlayerAbilityPostEnum,
	
	onDealDamageAddEnum,
	onDealDamageMultiEnum,
	onDealDamageSpecialEnum,
	onDealAttackDamagePostEnum,
	
	onEnemyAttackEnum,
	onEnemySkillEnum,
	
	onBuffExpireEnum,
	onBuffHolderDeathEnum,
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

public class onRoundEndTrigger : TriggerEvent
{
	public int Turn;
	
	public onRoundEndTrigger(int t)
	{
		Turn = t;
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

public class onDealDamageAddTrigger : TriggerEvent
{
	public Character ReceivingChar; 
	public int DamageAmount;
	
	public onDealDamageAddTrigger(Character RC, int d)
	{
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


