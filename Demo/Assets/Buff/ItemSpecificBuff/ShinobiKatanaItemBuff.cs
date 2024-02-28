using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;

namespace BuffUtil
{
    
public class ShinobiKatanaItemBuff : Buff
{
    public ShinobiKatanaItemBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onDealArmorDamagePostEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/ShinobiKatana");
    }
    
    public override void onApplication()
    {
        return;
    }
    
    public override string GetTooltipString()
    {
        return "Whenever you block an enemy attack, deal armor amount as poise damage";
    }
    
    public override void onExpire()
    {
        return;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onDealArmorDamagePostTrigger T = (onDealArmorDamagePostTrigger) E;
        if (T.ReceivingChar == BuffTarget)
        {
            BattleLogicHandler.LowerPoise((EnemyCharacter) T.AttackingChar, v);
        }
    }
}

}