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
    
public class ShinobiKatanaItem : GameItem
{
    public ShinobiKatanaItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/ShinobiKatana");
        this.ItemName = "Shinobi Katana";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new ShinobiKatanaItemBuff(this.ItemOwner, this.ItemOwner, null, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Whenever you block an enemy attack, deal armor amount as stamina damage";
    }
}

}