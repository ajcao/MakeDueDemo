using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class BluePhilospherStoneItemBuff : Buff
{
    public BluePhilospherStoneItemBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onSecondPostTurnEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/BluePhilospherStone");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "At the end of turn, all armor is turned into resolve";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onSecondPostTurnTrigger T = (onSecondPostTurnTrigger) E;
        Debug.Log(this.BuffTarget.GetType());
        Debug.Log(T.CharacterType);
        if (this.BuffTarget.GetType().IsSubclassOf(T.CharacterType))
        {
            PlayableCharacter PC = (PlayableCharacter) this.BuffTarget;
            BattleLogicHandler.GainResolve(PC, PC.getCurrentArmor());
        }
    }
}

}
