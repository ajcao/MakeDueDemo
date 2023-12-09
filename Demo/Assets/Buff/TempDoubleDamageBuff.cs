using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class TempDoubleDamageBuff : Buff
{
    public TempDoubleDamageBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onDealDamageMultiEnum;
        this.TriggerSecondary = TriggerEventEnum.onPlayerAttackEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityAttackTwice");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        return "Double damage on next " + this.Intensity.Value + " attacks";
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        
        if (E.GetType() == typeof(onDealDamageMultiTrigger))
        {
            onDealDamageMultiTrigger TE = (onDealDamageMultiTrigger) E;
            
            if (TE.AttackingChar == this.BuffTarget && this.Intensity > 0)
            {
                v = (int) (v * 2);
            }
        }
        
        else
        {
            onPlayerAttackTrigger TE = (onPlayerAttackTrigger) E;
            
            if (TE.AttackingPlayer == this.BuffTarget)
            {
                this.Intensity-=1;
                if (this.Intensity == 0)
                    this.PrepareBuffForDeletion();
            }
            
        }
    }
}

}
