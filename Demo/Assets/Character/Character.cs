using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffUtil;

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
	
	protected Sprite CharacterIcon;
	
	protected List<Buff> BuffList = new List<Buff>();
	//Used to decided when to redraw buff list
	public bool BuffListDirty = false;

	
	public bool isAlive()
	{
		return this.Alive;
	}
	
	public List<Buff> getBuffList()
	{
		return BuffList;
	}
	
	
	public int getCurrentHealth()
	{
		return this.CurrentHealth;
	}
	
	public void setCurrentHealth(int h)
	{
		this.CurrentHealth = h;
		
		if (this.CurrentHealth >= this.MaxHealth)
		{
			this.CurrentHealth = this.MaxHealth;
		}
		
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
	
	public Sprite getCharacterIcon()
	{
		return CharacterIcon;
	}
	
}

}