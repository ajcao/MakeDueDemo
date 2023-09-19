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
        this.isGlobal = false;
        this.ItemName = "Apaxe";
    }
    
    public override void OnApply()
    {
        Buff B = new ApaxeItemBuff(this.ItemOwner, this.ItemOwner, 10, null);
        BattleLogicHandler.OnBuffApply(B);
    }
}

}