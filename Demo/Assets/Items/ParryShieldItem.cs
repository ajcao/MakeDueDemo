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
        Buff B = new ParryShieldItemBuff(this.ItemOwner, this.ItemOwner, 10, null);
        BattleLogicHandler.OnBuffApply(B);
    }
}

}