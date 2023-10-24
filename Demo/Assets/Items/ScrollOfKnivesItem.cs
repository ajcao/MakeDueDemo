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
    
public class ScrollOfKnivesItem : GameItem
{
    public ScrollOfKnivesItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/ScrollOfKnives");
        this.ItemName = "Scroll Of Knives";
    }
    
    public override void OnPickup()
    {
    }
    
    public override void OnApply()
    {
        Buff B = new ScrollOfKnivesItemBuff(this.ItemOwner, this.ItemOwner, 10, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Whenever you play a skill, deal 10 damage to everyone";
    }
}

}