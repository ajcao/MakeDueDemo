using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class GainArmorBuff : Buff
{
    public GainArmorBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPreTurnEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/DefendIcon");
    }

    
    public override void onApplication()
    {
        BattleLogicHandler.GainArmor(this.BuffTarget, this.Intensity.Value);
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "At the start of the turn, gain " + this.Intensity.Value + " armor";
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPreTurnTrigger T = (onPreTurnTrigger) E;
        Debug.Log(this.BuffTarget.GetType());
        Debug.Log(T.CharacterType);
        if (this.BuffTarget.GetType().IsSubclassOf(T.CharacterType))
        {
            BattleLogicHandler.GainArmor(this.BuffTarget, this.Intensity.Value);
        }
    }
}

}
