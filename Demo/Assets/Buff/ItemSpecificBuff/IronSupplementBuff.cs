using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class IronSupplementBuff : Buff
{
    
    public IronSupplementBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onRoundEndEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/IronSupplement");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "At the end of the round, excess armor is turned a stack. When the stack reaches 100, heal 10";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onRoundEndTrigger TE = (onRoundEndTrigger) E;
        this.Intensity += this.BuffTarget.getCurrentArmor();
        while (this.Intensity >= 100)
        {
            this.Intensity -= 100;
            BattleLogicHandler.GainHealth(this.BuffTarget, 10);
        }
        
    }
}

}
