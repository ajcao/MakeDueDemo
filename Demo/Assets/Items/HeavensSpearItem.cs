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
    
public class HeavensSpearItem : GameItem
{
    public HeavensSpearItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/HeavensSpear");
        this.ItemName = "Heavens Spear";
    }
    
    public override void OnPickup()
    {
        this.ItemOwner.setAttackStat(this.ItemOwner.getAttackStat() + 50);
    }
    
    public override void OnApply()
    {
        Buff B = new HeavensSpearItemBuff(this.ItemOwner, this.ItemOwner, null, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Increase basic attack damage by 50. User can no longer heal. On user death allies takes huge damage";
    }
}

}