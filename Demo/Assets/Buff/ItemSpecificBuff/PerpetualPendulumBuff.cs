using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class PerpetualPendulumBuff : Buff
{
    public PerpetualPendulumBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onRoundEndEnum;
        this.TriggerSecondary = TriggerEventEnum.onResolveGainSpecialEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/PerpetualPendulum");
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
        string s1 = "Cooldoowns are reduced twice as fast. Resolve can no longer be gained";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        if (E.GetType() == typeof(onRoundEndTrigger))
        {
            onRoundEndTrigger TE = (onRoundEndTrigger) E;
            ((PlayableCharacter) this.BuffTarget).RefreshCasting();
        }
        
        else //Is character receiving resolve
        {
            onResolveGainSpecialTrigger TE = (onResolveGainSpecialTrigger) E;
            if (TE.ReceivingChar == BuffTarget)
            {
                v = 0;
            }
            
        }
    }
}

}
