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
    
public class ApaxeItem : GameItem
{
    public ApaxeItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/Apaxe");
        this.ItemName = "Apaxe";
    }
    
    public override void OnPickup()
    {
    }
    
    public override void OnApply()
    {
        Buff B = new ApaxeItemBuff(this.ItemOwner, this.ItemOwner, 20, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Increase damage by 20. Whenenver you basic attack, deal 20 damage to two random enemies";
    }
}

}