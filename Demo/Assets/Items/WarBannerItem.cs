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
    
public class WarBannerItem : GameItem
{
    public WarBannerItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/WarBanner");
        this.ItemName = "War Banner";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new WarBannerBuff(this.ItemOwner, this.ItemOwner, null, 1);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Every attack during the first turn, apply 2 vulnurable to all enemies";
    }
}

}