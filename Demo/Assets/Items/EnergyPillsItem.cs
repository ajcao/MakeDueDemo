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
    
public class EnergyPillsItem : GameItem
{
    public EnergyPillsItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/EnergyPills");
        this.ItemName = "Energy Pills";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new GainResolveBuff(this.ItemOwner, this.ItemOwner, this.ItemOwner.getMaxResolve(), 2);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain max resolve for first two turns";
    }
}

}