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
    
public class MetalRoseItem : GameItem
{
    public MetalRoseItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/MetalRose");
        this.ItemName = "Metal Rose";
    }
    
    public override void OnPickup()
    {
    }
    
    public override void OnApply()
    {
        Buff B = new SpikeBuff(this.ItemOwner, this.ItemOwner, 30, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain 30 spike";
    }
}

}