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
    
public class BlessedWineItem : GameItem
{
    public BlessedWineItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/BlessedWine");
        this.ItemName = "BlessedWine";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new BlessedWineItemBuff(this.ItemOwner, this.ItemOwner, 0, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Every 12th abilities cast, heal to full";
    }
}

}