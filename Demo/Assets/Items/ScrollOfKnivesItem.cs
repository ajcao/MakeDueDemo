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
        Buff B = new ScrollOfKnivesItemBuff(this.ItemOwner, this.ItemOwner, 0, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Every two times you play a skill, deal 30 damage to everyone";
    }
}

}