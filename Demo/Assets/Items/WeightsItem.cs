using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using UnityEngine.UI;
using TooltipUtil;
using BuffUtil;

namespace ItemUtil
{
    
public class WeightsItem : GameItem
{
    public WeightsItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/Weights");
        this.ItemName = "Weights";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new AttackUpBuff(this.ItemOwner, this.ItemOwner, 30, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain 30 Attack at the start of the battle";
    }
}

}