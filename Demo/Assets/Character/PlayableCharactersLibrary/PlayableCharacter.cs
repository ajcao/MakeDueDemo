using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityUtil;

namespace CharacterUtil
{
    

 //Playable Character Class
public abstract class PlayableCharacter : Character
{

	protected int AttackStat;
	protected int DefenseStat;
	
	protected int Mana;
	protected int ManaRegeneration;
	
	protected bool HasCasted;
	
	public List<Ability> AbilityPool;
	
	public int getMana()
	{
		return this.Mana;
	}
	
	public void setMana(int m)
	{
		this.Mana = m;
	}
	
	public int getAttackStat()
	{
		return AttackStat;
	}
	
	public int getDefenseStat()
	{
		return DefenseStat;
	}
	
	public void SetHasCasted(bool b)
	{
		HasCasted = b;
	}
	
	public bool GetHasCasted()
	{
		return HasCasted;
	}
	
	
	public List<Ability> getAbilityPool()
	{
		return AbilityPool;
	}
	
			
}

}
