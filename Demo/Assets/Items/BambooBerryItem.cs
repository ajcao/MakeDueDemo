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
    
public class BambooBerryItem : GameItem
{
    public BambooBerryItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/BambooBerry");
        this.ItemName = "Bamboo Berry";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new GainHPBuff(this.ItemOwner, this.ItemOwner, 20, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Gain 20 hp at start of every turn";
    }
}

}