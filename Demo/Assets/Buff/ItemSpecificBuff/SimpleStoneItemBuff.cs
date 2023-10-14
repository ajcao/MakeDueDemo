using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class SimpleStoneItemBuff : Buff
{
    public SimpleStoneItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerBasicAttackEnum;
        this.TriggerSecondary = TriggerEventEnum.onPlayerBasicDefendEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/SimpleStone");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Whenever you basic attack or defend,";
        string s2 = "Gain +2 dmg and +2 def";
        return s1 + "\n" + s2;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        
        if (E.GetType() == typeof(onPlayerBasicAttackTrigger))
        {
            onPlayerBasicAttackTrigger TE = (onPlayerBasicAttackTrigger) E;
            
            if (TE.AttackingPlayer == this.BuffTarget)
            {
                Buff B = new AttackUpBuff(BuffTarget, BuffTarget, this.Intensity.Value, null);
                BattleLogicHandler.OnBuffApply(B);
                
                B = new DefenseUpBuff(BuffTarget, BuffTarget, this.Intensity.Value, null);
                BattleLogicHandler.OnBuffApply(B);
            }
        }
        
        else //Did character gain armor
        {
            onPlayerBasicDefendTrigger TE = (onPlayerBasicDefendTrigger) E;
            
            if (TE.CastingPlayer == this.BuffTarget)
            {
                Buff B = new AttackUpBuff(BuffTarget, BuffTarget, this.Intensity.Value, null);
                BattleLogicHandler.OnBuffApply(B);
                
                B = new DefenseUpBuff(BuffTarget, BuffTarget, this.Intensity.Value, null);
                BattleLogicHandler.OnBuffApply(B);
            }
            
        }
    }
}

}
