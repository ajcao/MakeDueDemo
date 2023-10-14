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
        this.Trigger = TriggerEventEnum.noTriggerEnum;
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
        int Inten = this.Intensity.Value;
        BuffTarget.setDefenseOutputModifier(BuffTarget.getDefenseOutputModifier() + Inten);
    }
    
    public override void onExpire()
    {
        int Inten = this.Intensity.Value;
        BuffTarget.setDefenseOutputModifier(BuffTarget.getDefenseOutputModifier() - Inten);
    }
    
    public override string GetTooltipString()
    {
        return "Increase defense by " + this.Intensity.Value;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        return;
    }
}

}
