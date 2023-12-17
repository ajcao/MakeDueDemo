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
    
public class LeftScarabShellItem : GameItem
{
    public LeftScarabShellItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/LeftScarabShell");
        this.ItemName = "Left Scarab Shell";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new LeftScarabShellItemBuff(this.ItemOwner, this.ItemOwner, 1, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain 40 armor for turns 1-4";
    }
}

}