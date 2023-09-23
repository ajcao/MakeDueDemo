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
        this.ItemOwner.setAttackStat(this.ItemOwner.getAttackStat() + 20);
    }
    
    public override void OnApply()
    {
        Buff B = new ApaxeItemBuff(this.ItemOwner, this.ItemOwner, 20, null);
        BattleLogicHandler.OnBuffApply(B);
    }
}

}