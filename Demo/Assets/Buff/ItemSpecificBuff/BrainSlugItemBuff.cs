using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class BrainSlugItemBuff : Buff
{
    public BrainSlugItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onDealAttackDamagePostEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/BrainSlug");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "Players gain " + this.Intensity.Value + " resolve when attacking";
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onDealAttackDamagePostTrigger T = (onDealAttackDamagePostTrigger) E;
        if (T.AttackingChar == BuffTarget)
        {
            BattleLogicHandler.GainResolve((PlayableCharacter) T.AttackingChar, this.Intensity.Value);
        }
    }
}

}
