using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class BlessedWineItemBuff : Buff
{
    public BlessedWineItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerAbilityPostEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/BlessedWine");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Every 12th abilities cast, heal to full";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerAbilityPostTrigger T = (onPlayerAbilityPostTrigger) E;
        if (T.CastingPlayer == (PlayableCharacter) BuffTarget)
        {
            this.Intensity += 1;
            
            if (this.Intensity >= 12)
            {
                this.Intensity = 0;
                BattleLogicHandler.GainHealth(BuffTarget, BuffTarget.getMaxHealth());
                
            }
            
        }
        
    }
}

}
