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
    
public class ParryShieldItem : GameItem
{
    public ParryShieldItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/ParryShield");
        this.ItemName = "Parry Shield";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new ParryShieldItemBuff(this.ItemOwner, this.ItemOwner, 20, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Deal 20 damage whenever you gain armor or block another character";
    }
}

}