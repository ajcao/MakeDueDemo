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
    
public class LawBookItem : GameItem
{
    public LawBookItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/LawBook");
        this.ItemName = "Law Book";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new LawBookItemBuff(this.ItemOwner, this.ItemOwner, 0, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Every 8th skill cast, give 50% resolve to all allies";
    }
}

}