using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace TriggerEventUtil
{
	
	
public enum TriggerEventEnum
{
	onTurnStartEnum,
	onTurnEndEnum,
	onPlayerAttackEnum,
	onPlayerAbilityEnum,
	onEnemyAttackEnum,
	onDeathEnum
	
}

public abstract class TriggerEvent
{
}


public class onTurnStartTrigger : TriggerEvent
{
	public int Turn;
	
	public onTurnStartTrigger(int t)
	{
		Turn = t;
	}
}

public class onTurnEndTrigger : TriggerEvent
{
	public int Turn;
	
	public onTurnEndTrigger(int t)
	{
		Turn = t;
	}
}

public class onPlayerAttackTrigger : TriggerEvent
{
	public PlayableCharacter AttackingPlayer;
	public EnemyCharacter ReceivingEnemy;
	public int DamageAmount;
	
	public onPlayerAttackTrigger(PlayableCharacter P, EnemyCharacter E, int d)
	{
		AttackingPlayer = P;
		ReceivingEnemy = E;
		DamageAmount = d;
	}
}

public class onPlayerAbilityTrigger : TriggerEvent
{
	public PlayableCharacter CastingPlayer;
	public EnemyCharacter? ReceivingEnemy;
	
	public onPlayerAbilityTrigger(PlayableCharacter P, EnemyCharacter E)
	{
		CastingPlayer = P;
		ReceivingEnemy = E;
	}
}

public class onEnemyAttackTrigger : TriggerEvent
{
	public EnemyCharacter AttackingEnemy;
	public PlayableCharacter ReceivingPlayer; 
	public int DamageAmount;
	
	public onEnemyAttackTrigger(EnemyCharacter E, PlayableCharacter P, int d)
	{
		AttackingEnemy = E;
		ReceivingPlayer = P;
		DamageAmount = d;
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


