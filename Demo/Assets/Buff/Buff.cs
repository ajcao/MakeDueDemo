using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

public abstract class Buff
{
	TriggerEventEnum Trigger;
	Character? BuffTarget;
	Character OriginalBuffer;
    int? Intensity;
	int? Duration;
	
	public TriggerEventEnum? getTrigger()
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
	
	protected abstract bool CheckForConditions();
	public abstract void onApplication(TriggerEvent E);
	public abstract void onExpire();
	public abstract void onEffect(TriggerEvent E);
}
