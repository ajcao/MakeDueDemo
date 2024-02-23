using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityUtil;
using ItemUtil;

namespace CharacterUtil
{
    

 //Playable Character Class
public abstract class PlayableCharacter : Character
{

	protected int AttackStat;
	protected int DefenseStat;
	
	protected int Resolve;
	protected int MaxResolve;
	public int ResolveRegeneration;
	
	protected bool HasCasted;
	
	protected bool GetCharacterDataScene = true;
	
	
	public List<Ability> AbilityPool;
	
	public List<GameItem> Inventory = new List<GameItem>();
	
	public int getResolve()
	{
		return this.Resolve;
	}
	
	public void setResolve(int d)
	{
		Resolve = Mathf.Min(d, MaxResolve);
	}
	
	public int getMaxResolve()
	{
		return MaxResolve;
	}
	
	public void FullHealthResolveBonus()
	{
		if (this.CurrentHealth >= this.MaxHealth)
		{
			BattleLogicHandler.GainResolve(this, this.ResolveRegeneration);
		}
	}
	
	public int getAttackStat()
	{
		return AttackStat;
	}
	
	public void setAttackStat(int d)
	{
		AttackStat = d;
	}
	
	public int getDefenseStat()
	{
		return DefenseStat;
	}
	
	public void setDefenseStat(int d)
	{
		DefenseStat = d;
	}
	
	public void setHasCasted(bool b)
	{
		HasCasted = b;
	}
	
	public bool getHasCasted()
	{
		return HasCasted;
	}
	
	public bool IsAbletoCast()
	{
		//Player must be alive
		if (!this.isAlive())
		{
			return false;
		}
		
		//Player must not have casted
		if (this.getHasCasted())
		{
			return false;
		}
		
		foreach (Ability A in AbilityPool)
		{
			if (A.canCast())
			{
				return true;
			}
		}
		
		return false;
	}
	
	public void RefreshCasting()
	{
		foreach (Ability A in AbilityPool)
		{
			A.reduceCooldown();
		}
		this.setHasCasted(false);
	}
	
	
	public List<Ability> getAbilityPool()
	{
		return AbilityPool;
	}
	
	public void ResetAllCooldown()
	{
		foreach (Ability A in AbilityPool)
		{
			A.resetCooldown();
		}
	}
	

	public List<GameItem> GetIntentory()
	{
		return Inventory;
	}
	
	public void AddToInventory(GameItem I)
	{
		this.Inventory.Add(I);
		I.AssignCharacter(this);
		I.OnPickup();
	}
	
	public void ToggleCharacterData(bool B)
	{
		GetCharacterDataScene = B;
	}
	
	public abstract string GetLoreData();
	
	//Should probably move this to a more UI-focused script
	public void OnMouseDown()
	{
		//In a scene where players can access character data
		//eg: title screen, item selection scene
		if (GetCharacterDataScene)
		{
			GameObject DummyCharacter = Instantiate(this.gameObject, new Vector2(0.0f,0.0f), Quaternion.identity) as GameObject;
			DontDestroyOnLoad(DummyCharacter);
			DummyCharacter.name = "DummyCharacterGameObject";
			
			SceneCoordinator.LoadCharacterDataScene();
		}
	}
			
}

}
