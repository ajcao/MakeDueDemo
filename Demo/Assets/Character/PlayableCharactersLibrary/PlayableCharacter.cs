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
	
	protected int Resolve;
	protected int MaxResolve;
	
	//List of all characters who protected this characters
	//Charactres in this list will build resolve
	protected PlayableCharacter[] ProtectionList;
	
	protected bool HasCasted;
	
	public List<Ability> AbilityPool;
	
	public int getResolve()
	{
		return this.Resolve;
	}
	
	public void setMana(int r)
	{
		this.Resolve = r;
	}
	
	public int getAttackStat()
	{
		return AttackStat;
	}
	
	public int getDefenseStat()
	{
		return DefenseStat;
	}
	
	public void setHasCasted(bool b)
	{
		HasCasted = b;
	}
	
	public bool getHasCasted()
	{
		return HasCasted;
	}
	
	
	public List<Ability> getAbilityPool()
	{
		return AbilityPool;
	}
	

	
			
}

}
