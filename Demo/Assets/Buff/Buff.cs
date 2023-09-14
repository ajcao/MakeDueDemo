using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using UnityEngine.UI;
using TooltipUtil;

namespace BuffUtil
{
	
public abstract class Buff : IEquatable<Buff>
{
	protected TriggerEventEnum Trigger;
	protected Character BuffTarget;
	protected Character OriginalBuffer;
    protected int? Intensity;
	protected int? Duration;
	protected Sprite BuffIcon;
	public bool ToBeDeleted = false;
	public bool Stackable;
	
	protected GameObject BuffIndicator = null;
	
	public void AssignBuffIndicator(GameObject Indict)
	{
		this.BuffIndicator = Indict;
	}
	
	public GameObject GetBuffIndicator()
	{
		return BuffIndicator;
	}
	
	public bool Visible;
	
	public TriggerEventEnum getTrigger()
	{
		return this.Trigger;
	}
	
	public Character getBuffTarget()
	{
		return this.BuffTarget;
	}
	
	public Character getOriginalBuffer()
	{
		return this.OriginalBuffer;
	}
	
	public void setOriginalBuffer(Character C)
	{
		this.OriginalBuffer = C;
	}
	
	public int? getIntensity()
	{
		return this.Intensity;
	}
	
	public int? getDuration()
	{
		return this.Duration;
	}
	
	public void decrementDuration()
	{
		if ( this.Duration.HasValue )
		{
			this.Duration-=1;
			if ( this.Duration.Value == 0 )
			{
				this.onExpire();
				this.PrepareBuffForDeletion();
			}
		}
	}
	
	public void decrementIntensity()
	{
		if (this.Intensity.HasValue)
		{
			this.Intensity-=1;
			if ( this.Intensity.Value == 0 )
			{
				//Put a buff on expire trigger event here?
				this.onExpire();
				this.PrepareBuffForDeletion();
			}
		}
	}
	
	public Sprite getIcon()
	{
		return BuffIcon;
	}
	
	//Returns true is stacking was successful
	protected bool PerformStacking(Character Buffer, Character Target)
	{
		if (!this.Stackable)
		{
			return false;
		}
		
		//Perform Intensity stacking on Infinite Duration buffs
		if (!this.getDuration().HasValue)
		{
			foreach (Buff B in Target.getBuffList())
			{
				if (B.GetType() == this.GetType() && B.getDuration() == this.getDuration())
				{
					B.setOriginalBuffer(Buffer);
					B.StackIntensity(this.getIntensity().Value);
					return true;
				}
			}
		}
		
		//Perform Duration stacking on non-Intensity buffs
		if (!this.getIntensity().HasValue)
		{
			foreach (Buff B in Target.getBuffList())
			{
				if (B.GetType() == this.GetType() && B.getIntensity() == this.getIntensity())
				{
					B.setOriginalBuffer(Buffer);
					B.ExtendDuration(this.getDuration().Value);
					return true;
				}
			}
		}
		
		
		return false;
	}
	
	public void StackIntensity(int I)
	{
		this.Intensity += I;
	}
	
	public void ExtendDuration(int D)
	{
		this.Duration += D;
	}
	
	public void PrepareBuffForDeletion()
	{
		this.ToBeDeleted = true;
		
		//Since Buffs and BuffIndicator are closely linked
        //Properly set null references as "garbage" collection
        GameObject.Destroy(BuffIndicator);
        BuffIndicator = null;
	}
	
    public abstract string GetTooltipString();
	
	public void onApplicationWrapper()
	{
		this.onApplication();
		//If no stacking is done, add buff to BuffHandler
		if (!this.PerformStacking(OriginalBuffer, BuffTarget))
        {
            BuffHandler.AddBuff(this, BuffTarget);
        }
	}
	public abstract void onApplication();
	public abstract void onExpire();
	public abstract void onTriggerEffect(TriggerEvent E, ref int v);
	
	public bool Equals(Buff B)
	{
		return ( (this.GetType() == B.GetType()) && (this.getIntensity() == B.getIntensity()) && (this.getDuration() == B.getDuration()) && (this.getBuffTarget() == B.getBuffTarget()) );
	}
}

}