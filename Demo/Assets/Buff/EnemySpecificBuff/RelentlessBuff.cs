using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class RelentlessBuff : Buff
{
    public RelentlessBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.noTriggerEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/RelentlessBuff");
    }

    
    public override void onApplication()
    {
    }
    
    //If user is death damage all allies
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Enemy is relentless. It will not go down easy...";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
    }
}

}
