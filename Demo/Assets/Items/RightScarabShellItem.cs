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
    
public class RightScarabShellItem : GameItem
{
    public RightScarabShellItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/RightScarabShell");
        this.ItemName = "Right Scarab Shell";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new RightScarabShellItemBuff(this.ItemOwner, this.ItemOwner, 1, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain 60 armor for turns 5-8";
    }
}

}