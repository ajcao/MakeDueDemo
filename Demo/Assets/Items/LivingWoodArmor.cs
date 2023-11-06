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
    
public class LivingWoodArmorItem : GameItem
{
    public LivingWoodArmorItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/LivingWoodArmor");
        this.ItemName = "Living Wood Armor";
    }
    
    public override void OnPickup()
    {
    }
    
    public override void OnApply()
    {
        Buff B = new GainArmorBuff(this.ItemOwner, this.ItemOwner, 20, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain 20 armor at the end of every round";
    }
}

}