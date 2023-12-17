using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class ScarabHeadItemBuff : Buff
{
    public ScarabHeadItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPreTurnEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/ScarabHead");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Gain 80 armor for Round 9-12. \n";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPreTurnTrigger T = (onPreTurnTrigger) E;
        int roundNum = BattleSceneHandler.GetRound();
        this.Intensity = roundNum;
        Debug.Log(roundNum);
        //Ensure it is the player's turn
        if (this.BuffTarget.GetType().IsSubclassOf(T.CharacterType))
        {
            if ((9 <= roundNum) && (roundNum <= 12))
            {
                BattleLogicHandler.BuffGainArmor(this.BuffTarget, 80);
            }
            else if (roundNum > 12)
            {
				this.PrepareBuffForDeletion();
            }
        }
    }
}

}
