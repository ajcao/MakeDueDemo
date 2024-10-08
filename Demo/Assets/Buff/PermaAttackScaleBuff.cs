using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class PermaAttackScaleBuff : Buff
{
    public PermaAttackScaleBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPreTurnEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/PermaScaleBuff");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Every turn gain 5 Attack";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPreTurnTrigger T = (onPreTurnTrigger) E;
        if (this.BuffTarget.GetType().IsSubclassOf(T.CharacterType))
        {
            EnemyCharacter EC = this.BuffTarget as EnemyCharacter;
            AttackUpBuff AB = new AttackUpBuff(EC, EC, 5, null);
            BattleLogicHandler.OnBuffApply(AB);
        }

    }
}

}
