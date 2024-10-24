using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class DamagedFormBuff : Buff
{
    public DamagedFormBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onHealthDamageWasTakenEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/DamagedBuff");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "When target's hp is under " + this.Intensity + ", become damaged";
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onHealthDamageWasTakenTrigger T = (onHealthDamageWasTakenTrigger) E;
        if (T.ReceivingChar == BuffTarget)
        {
            if (BuffTarget.getCurrentHealth() <= this.Intensity.Value)
            {
                //Change phase move?
                this.PrepareBuffForDeletion();
                EnemyCharacter Enem = (EnemyCharacter) BuffTarget;
                Enem.EnterNewForm("Damaged");
            }
        }
    }
}

}
