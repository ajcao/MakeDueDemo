using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class AttackUpBuff : Buff
{
    public AttackUpBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onDealDamageAddEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/AttackIcon");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        return "Increase damage by " + this.Intensity.Value;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onDealDamageAddTrigger T = (onDealDamageAddTrigger) E;
        if (T.AttackingChar == BuffTarget)
        {
            v = (int) (v + this.Intensity.Value);
        }
    }
}

}
