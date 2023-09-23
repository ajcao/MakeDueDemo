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
    
public class MindAmuletItem : GameItem
{
    public MindAmuletItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/MindAmulet");
        this.ItemName = "Mind Amulet";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new GainResolveBuff(this.ItemOwner, this.ItemOwner, 20, null);
        BattleLogicHandler.OnBuffApply(B);
    }
}

}