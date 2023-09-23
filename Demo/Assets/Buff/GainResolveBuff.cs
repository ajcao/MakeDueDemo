using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class GainResolveBuff : Buff
{
    public GainResolveBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPreTurnEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/GenericGiveResolve");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "At the start of the turn, gain " + this.Intensity.Value + " resolve";
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPreTurnTrigger T = (onPreTurnTrigger) E;
        Debug.Log(this.BuffTarget.GetType());
        Debug.Log(T.CharacterType);
        if (this.BuffTarget.GetType().IsSubclassOf(T.CharacterType))
        {
            BattleLogicHandler.GainResolve((PlayableCharacter) this.BuffTarget, this.Intensity.Value);
        }
    }
}

}
