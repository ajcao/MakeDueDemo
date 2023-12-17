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
    
public class ArmorRepairKitItem : GameItem
{
    public ArmorRepairKitItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/ArmorRepairKit");
        this.ItemName = "Armor Repair Kit";
    }
    
    public override void OnPickup()
    {
    }
    
    public override void OnApply()
    {
        Buff B = new RetainBuff(this.ItemOwner, this.ItemOwner, 40, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Retain 40 armor";
    }
}

}