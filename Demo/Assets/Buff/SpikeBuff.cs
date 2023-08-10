using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class SpikeBuff : Buff
{
    public SpikeBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onEnemyAttackEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/GainSpike");
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
    
    public override string GetTooltipString()
    {
        return "Deal " + this.Intensity.Value + " dmg to Attackers";
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override void onTriggerEffect(TriggerEvent E)
    {
        onEnemyAttackTrigger T = (onEnemyAttackTrigger) E;
        if (T.ReceivingPlayer == BuffTarget)
        {
            BattleLogicHandler.Damage(T.AttackingEnemy, this.Intensity.Value);
        }
    }
}

}