using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class ToxicSporeBuff : Buff
{
    public ToxicSporeBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerAttackEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/ToxicSpore");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "When this enemy is hurt, take an additional " + this.Intensity + " damage";
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerAttackTrigger T = (onPlayerAttackTrigger) E;
        if (T.ReceivingEnemy == BuffTarget)
        {
            BattleLogicHandler.Damage(BuffTarget, this.Intensity.Value);
        }
    }
}

}
