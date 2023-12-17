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
    
public class ScarabHeadItem : GameItem
{
    public ScarabHeadItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/ScarabHead");
        this.ItemName = "Scarab Head";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new ScarabHeadItemBuff(this.ItemOwner, this.ItemOwner, 1, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain 80 armor for turns 9-12";
    }
}

}