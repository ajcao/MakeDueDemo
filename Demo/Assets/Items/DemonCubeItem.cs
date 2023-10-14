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
    
public class DemonCubeItem : GameItem
{
    public DemonCubeItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/DemonsCube");
        this.ItemName = "DemonCube";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new DemonCubeItemBuff(this.ItemOwner, this.ItemOwner, 0, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Every 10 turns, deal 200 damage to all enemies";
    }
}

}