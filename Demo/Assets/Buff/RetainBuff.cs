using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class RetainBuff : Buff
{
    public RetainBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.noTriggerEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/Defend3Icon");
    }
    
    public override void onApplication()
    {
        this.BuffTarget.setArmorRetain(this.BuffTarget.getArmorRetain() + this.Intensity.Value);
    }
    
    public override string GetTooltipString()
    {
        return "Retain " + this.Intensity + " armor at the start of the next turn";
    }
    
    public override void onExpire()
    {
        this.BuffTarget.setArmorRetain(this.BuffTarget.getArmorRetain() - this.Intensity.Value);
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
    }
}

}