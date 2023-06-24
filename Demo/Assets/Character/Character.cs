using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterUtil
{
	
	
//Abstract Character Class
public abstract class Character : MonoBehaviour
{
	
	protected bool Alive;
	
	protected int CurrentHealth;
	protected int MaxHealth;
	
	protected int CurrentArmor;
	//Max armor retained at end of round
	protected int ArmorRetain;
	
	protected int DamageOutputModifier;
	
	public bool isAlive()
	{
		return this.Alive;
	}
	
	public int getCurrentHealth()
	{
		return this.CurrentHealth;
	}
	
	public void setCurrentHealth(int h)
	{
		this.CurrentHealth = h;
		if (this.CurrentHealth <= 0)
		{
			BattleLogicHandler.CharacterDies(this);
			this.onDeath();
		}
	}
	
	public int getMaxHealth()
	{
		return this.MaxHealth;
	}
	
	public void setMaxHealth(int h)
	{
		this.MaxHealth = h;
	}
	
	public int getCurrentArmor()
	{
		return this.CurrentArmor;
	}
	
	public void setCurrentArmor(int a)
	{
		this.CurrentArmor = a;
	}
	
	public int getArmorRetain()
	{
		return this.ArmorRetain;
	}
	
	public void setArmorRetain(int a)
	{
		this.ArmorRetain = a;
	}
	
	public int getDamageOutputModifier()
	{
		return this.DamageOutputModifier;
	}
	
	public void setDamageOutputModifier(int d)
	{
		this.DamageOutputModifier = d;
	}
	
	public void onDeath()
	{
		this.Alive = false;
	}
	
}

}