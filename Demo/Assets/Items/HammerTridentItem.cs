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
    
public class HammerTridentItem : GameItem
{
    public HammerTridentItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/HammerTrident");
        this.ItemName = "Hammer Trident";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new HammerTridentItemBuff(this.ItemOwner, this.ItemOwner, 0, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Every 8th attack deals double damage";
    }
}

}