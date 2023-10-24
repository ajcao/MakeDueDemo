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
    
public class BluePhilospherStoneItem : GameItem
{
    public BluePhilospherStoneItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/BluePhilospherStone");
        this.ItemName = "Blue Philospher Stone";
    }
    
    public override void OnPickup()
    {
    }
    
    public override void OnApply()
    {
        Buff B = new BluePhilospherStoneItemBuff(this.ItemOwner, this.ItemOwner, null, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "At the end of turn, all armor is turned into resolve";
    }
}

}