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
    
    protected override bool CheckForConditions()
    {
        foreach (Buff B in BuffTarget.getBuffList())
        {
            if (B.GetType() == this.GetType() && B.getDuration() == this.getDuration())
            {
                this.onStacking(B);
                return false;
            }
        }
        return true;
    }
    
    public override void onApplication()
    {
        //Is this the first stack?
        if (this.CheckForConditions())
        {
            int Inten = this.Intensity.Value;
            BuffTarget.setDamageOutputModifier(BuffTarget.getDamageOutputModifier() + Inten);
            BuffTarget.getBuffList().Add(this);
            BattleLogicHandler.getBuffsList()[this.Trigger].Add(this);
            this.setDirty();
        }
    }
    
    public override void onStacking(Buff B)
    {
        AttackUpBuff OrigB = (AttackUpBuff) B;
        this.Intensity += OrigB.Intensity;
        B.onExpire();
        this.onApplication();
    }
    
    public override void onExpire()
    {
        int Inten = this.Intensity.Value;
        BuffTarget.setDamageOutputModifier(BuffTarget.getDamageOutputModifier() - Inten);
        BuffTarget.getBuffList().Remove(this);
        BattleLogicHandler.getBuffsList()[this.Trigger].Remove(this);
        //Probably handles deleting the buff icon too
        this.setDirty();
    }
    
    public override void onEffect(TriggerEvent E)
    {
        return;
    }
}

}
