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
    
public class SoulContractItem : GameItem
{
    public SoulContractItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/SoulContract");
        this.ItemName = "Soul Contract";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new GainResolveBuff(this.ItemOwner, this.ItemOwner, this.ItemOwner.getMaxResolve() / 2, null);
        BattleLogicHandler.OnBuffApply(B);
        
        B = new SoulContractDamageDebuff(this.ItemOwner, this.ItemOwner, 5, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Every turn gain 50% resolve. Every turn lose 5 hp and incrase this amount by 5";
    }
}

}