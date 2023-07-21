using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using UnityEngine.UI;

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
	
	public bool Visible;
	
	public TriggerEventEnum getTrigger()
	{
		return this.Trigger;
	}
	
	public Character? getBuffTarget()
	{
		return this.BuffTarget;
	}
	
	public Character getOriginalBuffer()
	{
		return this.OriginalBuffer;
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
		if (this.Duration.HasValue)
		{
			this.Duration-=1;
			if (this.Duration.Value == 0)
			{
				this.onExpire();
			}
		}
	}
	
	public Sprite getIcon()
	{
		return BuffIcon;
	}
	
	public void setDirty()
	{
		if (this.Visible == true)
		{
			BuffTarget.BuffListDirty = true;
		}
	}
	
	protected abstract bool CheckForConditions();
	
	public abstract void onApplication();
	public abstract void onStacking(Buff B);
	public abstract void onExpire();
	public abstract void onEffect(TriggerEvent E);
}

}