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
    
public class HardhatItem : GameItem
{
    public HardhatItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/Hardhat");
        this.ItemName = "Hardhat";
    }
    
    public override void OnPickup()
    {
    }
    
    public override void OnApply()
    {
        Buff B = new DefenseUpBuff(this.ItemOwner, this.ItemOwner, 30, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain 30 Defense at the start of the battle";
    }
}

}