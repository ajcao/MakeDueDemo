using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class EnragedFormBuff : Buff
{
    public EnragedFormBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onHealthDamageWasTakenEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/EnragedFormBuff");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "After taking " + this.Intensity.Value + " health damage, become enraged";
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
            this.Intensity = Mathf.Max(0, this.Intensity.Value - T.DamageAmount);
            if (this.Intensity.Value <= 0)
            {
                //Change phase move?
                this.PrepareBuffForDeletion();
                EnemyCharacter Enem = (EnemyCharacter) BuffTarget;
                Enem.EnterNewForm("Enranged");
            }
        }
    }
}

}
