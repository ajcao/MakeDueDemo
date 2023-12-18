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
    
public class BatteryItem : GameItem
{
    public BatteryItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/Battery");
        this.ItemName = "Battery";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new BatteryItemBuff(this.ItemOwner, this.ItemOwner, 0, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Every 12th abilities cast, refresh all cooldown";
    }
}

}