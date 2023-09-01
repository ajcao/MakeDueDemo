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
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/DefendIcon");
    }

    
    public override void onApplication()
    {
        return;
    }
    
    public override void onExpire()
    {
        BattleLogicHandler.Armor(this.BuffTarget, this.Intensity.Value);
    }
    
    public override string GetTooltipString()
    {
        return "At the start of the round, gain " + this.Intensity.Value + " armor";
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        Debug.Log("Gaining " + this.Intensity.Value + " armor");
        BattleLogicHandler.Armor(this.BuffTarget, this.Intensity.Value);
    }
}

}
