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
    
public class SimpleStoneItem : GameItem
{
    public SimpleStoneItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/SimpleStone");
        this.ItemName = "Simple Stone";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new SimpleStoneItemBuff(this.ItemOwner, this.ItemOwner, 5, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Whenever you basic attack, gain +5 dmg";
    }
}

}