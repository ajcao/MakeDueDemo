using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class SoulContractDamageDebuff : Buff
{
    public SoulContractDamageDebuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onRoundEndEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/SoulDamageDebuff");
    }

    
    public override void onApplication()
    {
        return;
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Every turn take " + this.Intensity + " damage and increase this amount by 5 to a max of 50";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onRoundEndTrigger TE = (onRoundEndTrigger) E;
        BattleLogicHandler.BuffDamage(this.BuffTarget, this.Intensity.Value);
        this.Intensity = Mathf.Min(50, this.Intensity.Value + 5);
    }
}

}
