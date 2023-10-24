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
    
public class MagicMissileCannedItem : GameItem
{
    public MagicMissileCannedItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/MagicMissileCanned");
        this.ItemName = "Magic Missile in a Can";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new MagicMissileCannedItemBuff(this.ItemOwner, this.ItemOwner, 30, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Whenever you proc resolve, deal 30 damage to random enemy";
    }
}

}