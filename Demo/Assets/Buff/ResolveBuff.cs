using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class ResolveBuff : Buff
{
    public ResolveBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerAbilityPostEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/ActivateResolve");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "After casting an ability, decrease all cooldowns and go again";
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerAbilityPostTrigger T = (onPlayerAbilityPostTrigger) E;
        if (T.CastingPlayer == (PlayableCharacter) BuffTarget)
        {
            T.CastingPlayer.RefreshCasting();
            this.decrementIntensity();
            if (this.Intensity.Value <= 0)
            {
                this.ToBeDeleted = true;
            }
            
        }
    }
}

}