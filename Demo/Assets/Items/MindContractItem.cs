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
    
public class MindContractItem : GameItem
{
    public MindContractItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/MindContract");
        this.ItemName = "Mind Contract";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new GainResolveBuff(this.ItemOwner, this.ItemOwner, this.ItemOwner.getMaxResolve() / 2, null);
        BattleLogicHandler.OnBuffApply(B);
        
        B = new MindContractDamageDebuff(this.ItemOwner, this.ItemOwner, 5, null);
        BattleLogicHandler.OnBuffApply(B);

    }
    
    public override string GetTooltipString()
    {
        return "Every turn gain 50% resolve. Every turn lose 5 hp";
    }
}

}