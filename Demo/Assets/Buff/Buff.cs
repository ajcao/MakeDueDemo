using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using UnityEngine.UI;
using TooltipUtil;

namespace BuffUtil
{
	
public abstract class Buff
{
	protected TriggerEventEnum Trigger;
	protected Character BuffTarget;
	protected Character OriginalBuffer;
    protected int? Intensity;
	protected int? Duration;
	protected Sprite BuffIcon;
	public bool ToBeDeleted = false;
	
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
	
	public Sprite getIcon()
	{
		return BuffIcon;
	}
	
	protected bool PerformIntensityStacking(Character Buffer, Character Target, int I)
	{
		foreach (Buff B in Target.getBuffList())
		{
			if (B.GetType() == this.GetType() && B.getDuration() == this.getDuration())
            {
				B.setOriginalBuffer(Buffer);
                B.StackIntensity(I);
                return true;
            }
        }
		return false;
	}
	
	protected bool PerformDurationExtension(Character Buffer, Character Target, int D)
	{
		foreach (Buff B in Target.getBuffList())
		{
			if (B.GetType() == this.GetType())
            {
				B.setOriginalBuffer(Buffer);
                B.StackIntensity(D);
                return true;
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
	public abstract void onApplication();
	public abstract void onExpire();
	public abstract void onTriggerEffect(TriggerEvent E);
}

}