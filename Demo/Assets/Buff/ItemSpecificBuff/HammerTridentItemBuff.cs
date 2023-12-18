using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class HammerTridentItemBuff : Buff
{
    public HammerTridentItemBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerAttackEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/HammerTrident");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Every 8th attack deals double damage";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerAttackTrigger T = (onPlayerAttackTrigger) E;
        if (T.AttackingPlayer == BuffTarget)
        {
            this.Intensity += 1;
            
            if (this.Intensity == 7)
            {
                Buff B = new TempDoubleDamageBuff(BuffTarget, BuffTarget, 1, null);
                BattleLogicHandler.OnBuffApply(B);
            }
            if (this.Intensity >= 8)
            {
                this.Intensity = 0;
            }
        }
    }
}

}
