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
	protected bool[] ProtectionList = new bool[]{false, false, false, false};
	
	protected bool HasCasted;
	protected bool ResolveProc = false;
	
	public List<Ability> AbilityPool;
	
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
	
	public bool[] getProtectionList()
	{
		return ProtectionList;
	}
	
	public void setProtectionList(PlayableCharacter C)
	{
		int i = PlayerParty.getPartyIndex(C.gameObject);
		ProtectionList[i] = true;
	}
	
	public void resetProtectionList()
	{
		ProtectionList[0] = false;
		ProtectionList[1] = false;
		ProtectionList[2] = false;
		ProtectionList[3] = false;
		
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
	
	public void GiveResolve(int d)
	{
		int count = 0;
		foreach (bool b in ProtectionList)
		{
			if (b)
			{
				count++;
			}
		}
		
		for (int i = 0; i < ProtectionList.Length; i++)
		{
			if (ProtectionList[i])
			{
				PlayableCharacter P = PlayerParty.getPartyMember(i).GetComponent<PlayableCharacter>();
				P.setResolve(P.getResolve() + (d/count));
			}
		}
	}
	
	public void CheckResolve()
	{
		if (this.Resolve >= this.MaxResolve)
		{
			this.Resolve = 0;
			this.ResolveProc = true;
		}
	}
	
	public void ProcResolve()
	{
		if (this.ResolveProc)
		{
			this.ResolveProc = false;
			this.RefreshCasting();
		}
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
	

	
			
}

}
