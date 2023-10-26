using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class DefenseUpBuff : Buff
{
    public DefenseUpBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onArmorGainAddEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/DefendIcon");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        return "Increase defense by " + this.Intensity.Value;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onArmorGainAddTrigger T = (onArmorGainAddTrigger) E;
        if (T.CastingChar == BuffTarget)
        {
            v = (int) (v + this.Intensity.Value);
        }
    }
}

}
