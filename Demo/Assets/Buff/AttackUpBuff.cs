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
        this.Trigger = TriggerEventEnum.noTriggerEnum;
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
        this.BuffTarget.setDamageOutputModifier(this.BuffTarget.getDamageOutputModifier() + this.Intensity.Value);
    }
    
    public override void onExpire()
    {
        this.BuffTarget.setDamageOutputModifier(this.BuffTarget.getDamageOutputModifier() - this.Intensity.Value);
    }
    
    public override string GetTooltipString()
    {
        return "Increase damage by " + this.Intensity.Value;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
    }
}

}
