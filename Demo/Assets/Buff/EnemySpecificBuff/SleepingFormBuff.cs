using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class SleepingFormBuff : Buff
{
    public SleepingFormBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPoiseWasLostEnum;
        this.TriggerSecondary = TriggerEventEnum.onHealthDamageWasTakenEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/SleepingBuff");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "Upon taking any damage or the buff ending, wake up";
    }
    
    public override void onExpire()
    {
        EnemyCharacter Enem = (EnemyCharacter) this.BuffTarget;
        this.WakeUp(Enem);
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        
        if (E.GetType() == typeof(onPoiseWasLostTrigger))
        {
            onPoiseWasLostTrigger TE = (onPoiseWasLostTrigger) E;
            
            if (TE.ReceivingChar == this.BuffTarget)
            {
                //Change phase move?
                this.PrepareBuffForDeletion();
                EnemyCharacter Enem = (EnemyCharacter) BuffTarget;
                this.WakeUp(Enem);
            }
        }
        
        else //Did character gain armor
        {
            onHealthDamageWasTakenTrigger TE = (onHealthDamageWasTakenTrigger) E;
            
            if (TE.ReceivingChar == this.BuffTarget)
            {
                //Change phase move?
                this.PrepareBuffForDeletion();
                EnemyCharacter Enem = (EnemyCharacter) BuffTarget;
                this.WakeUp(Enem);
            }
            
        }
    }
    
    private void WakeUp(EnemyCharacter E)
    {
        if (E.Forms == "Sleeping")
        {
            E.EnterNewForm("Awake");
        }
    }
}

}
