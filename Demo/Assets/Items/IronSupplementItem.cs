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
    
public class IronSupplementItem : GameItem
{
    public IronSupplementItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/IronSupplement");
        this.ItemName = "Iron Supplement";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new IronSupplementBuff(this.ItemOwner, this.ItemOwner, 0, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "At the end of the round, excess armor is turned a stack. When the stack reaches 100, heal 30";
    }
}

}