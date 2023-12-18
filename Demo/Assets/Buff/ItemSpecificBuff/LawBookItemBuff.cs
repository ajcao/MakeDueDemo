using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class LawBookItemBuff : Buff
{
    public LawBookItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerAbilityPostEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/LawBook");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Every 8th skill cast, give 50% resolve to all allies";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerSkillTrigger T = (onPlayerSkillTrigger) E;
        if (T.CastingPlayer == (PlayableCharacter) BuffTarget)
        {
            this.Intensity += 1;
            
            if (this.Intensity >= 8)
            {
                this.Intensity = 0;
                foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
                {
                    PlayableCharacter P = G.GetComponent<PlayableCharacter>();
                    BattleLogicHandler.GainResolve(P, P.ResolveRegeneration);
                }
                
            }
            
        }
        
    }
}

}
