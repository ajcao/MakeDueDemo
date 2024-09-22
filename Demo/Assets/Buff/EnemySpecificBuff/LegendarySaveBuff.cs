using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class LegendarySaveBuff : Buff
{
    public LegendarySaveBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onDealPoiseDamageSpecialEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/LegendarySaveBuff");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "When the enemy's poise break, reset it without the enemy getting stunned";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onDealPoiseDamageSpecialTrigger T = (onDealPoiseDamageSpecialTrigger) E;
        if (T.ReceivingChar == (EnemyCharacter) BuffTarget)
        {
            if ( (T.ReceivingChar.getPoise() - T.PoiseAmount) <= 0)
            {
                    v = -(T.ReceivingChar.getMaxPoise());
                    this.Intensity -= 1;
                    if (this.Intensity <= 0)
                    {
                        this.ToBeDeleted = true;
                    }
            }
            
        }
        
    }
}

}
