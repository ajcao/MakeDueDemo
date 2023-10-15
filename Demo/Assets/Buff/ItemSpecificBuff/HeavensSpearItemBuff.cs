using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class HeavensSpearItemBuff : Buff
{
    public HeavensSpearItemBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onHealthGainSpecialEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/HeavensSpear");
    }

    
    public override void onApplication()
    {
    }
    
    //If user is death damage all allies
    public override void onExpire()
    {
        if (!this.BuffTarget.isAlive())
        {        
            foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
            {
                PlayableCharacter PC = G.GetComponent<PlayableCharacter>();
                if (PC.isAlive())
                {
                    BattleLogicHandler.BuffDamage(PC, 200);
                }
            }
            
        }
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Increase basic attack damage by 50. User can no longer heal. On user death allies takes huge damage";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onHealthGainSpecialTrigger T = (onHealthGainSpecialTrigger) E;
        if (T.ReceivingChar == BuffTarget)
        {
            v = 0;
        }
    }
}

}
