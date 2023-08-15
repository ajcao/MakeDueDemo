using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class GainArmorBuff : Buff
{
    public GainArmorBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onTurnStartEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/DefendIcon");
    }

    
    public override void onApplication()
    {
        int Inten = this.Intensity.Value;
        if (this.PerformIntensityStacking(OriginalBuffer, BuffTarget, Inten))
        {
            this.PrepareBuffForDeletion();
        }
        else
        {
            BuffHandler.AddBuff(this, BuffTarget);
        }
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        return "At the start of the round, gain " + this.Intensity.Value + " armor";
    }
    
    public override void onTriggerEffect(TriggerEvent E)
    {
        BattleLogicHandler.Armor(this.BuffTarget, this.Intensity.Value);
    }
}

}
