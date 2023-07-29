using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class NullBuff : Buff
{
    public NullBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/NullIcon");
    }
    
    public override void onApplication()
    {
        BuffTarget.getBuffList().Add(this);
        BattleLogicHandler.getBuffsList()[this.Trigger].Add(this);
    }
    
    public override void onExpire()
    {
        BuffTarget.getBuffList().Remove(this);
        BattleLogicHandler.getBuffsList()[this.Trigger].Remove(this);
        
        this.DeleteBuff();
    }
    
    public override void onTriggerEffect(TriggerEvent E)
    {
        return;
    }
}

}

