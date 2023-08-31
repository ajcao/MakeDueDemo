using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class GiveHPWhenAttackedDebuff : Buff
{
    public GiveHPWhenAttackedDebuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerAttackEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/ApplyLifeStealImage");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "Players heal " + this.Intensity.Value + " hp when attacking this enemy";
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
            T.AttackingPlayer.setCurrentHealth(T.AttackingPlayer.getCurrentHealth() + this.Intensity.Value);
        }
    }
}

}
