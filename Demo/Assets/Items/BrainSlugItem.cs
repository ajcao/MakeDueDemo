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
    
public class BrainSlugItem : GameItem
{
    public BrainSlugItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/BrainSlug");
        this.ItemName = "Brain Slug";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new BrainSlugItemBuff(this.ItemOwner, this.ItemOwner, 20, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Whenever you deal damage via attacks, gain 20 resolve";
    }
}

}