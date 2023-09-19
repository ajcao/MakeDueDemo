using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class StunnedBuff : Buff
{
    public StunnedBuff(Character CTarget, Character CBuffer, int? Inten, int Dur) 
    {
        this.Trigger = TriggerEventEnum.onDealDamageMultiEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/StunIcon");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "Take Double Damage from Abilities";
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onDealDamageMultiTrigger T = (onDealDamageMultiTrigger) E;
        if (T.ReceivingChar == BuffTarget)
        {
            v = v * 2;
        }
    }
}

}
