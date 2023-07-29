using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class AttackUpBuff : Buff
{
    public AttackUpBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/AttackIcon");
    }

    
    public override void onApplication()
    {
        int Inten = this.Intensity.Value;
        BuffTarget.setDamageOutputModifier(BuffTarget.getDamageOutputModifier() + Inten);
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
        int Inten = this.Intensity.Value;
        BuffTarget.setDamageOutputModifier(BuffTarget.getDamageOutputModifier() - Inten);
    }
    
    public override void onTriggerEffect(TriggerEvent E)
    {
        return;
    }
}

}
