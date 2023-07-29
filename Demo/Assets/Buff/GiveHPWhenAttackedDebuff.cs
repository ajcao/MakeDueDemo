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
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/ApplyLifeStealImage");
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
            BuffTarget.getBuffList().Add(this);
            BattleLogicHandler.getBuffsList()[this.Trigger].Add(this);
        }
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override void onTriggerEffect(TriggerEvent E)
    {
        onPlayerAttackTrigger T = (onPlayerAttackTrigger) E;
        if (T.ReceivingEnemy == BuffTarget)
        {
            T.AttackingPlayer.setCurrentHealth(T.AttackingPlayer.getCurrentHealth() + this.Intensity.Value);
        }
    }
}

}
