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
    
public class MusicalHammerItem : GameItem
{
    int PoiseAmt = 100;
    public MusicalHammerItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/MusicalHammer");
        this.ItemName = "Musical Hammer";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new MusicalHammerBuff(this.ItemOwner, this.ItemOwner, PoiseAmt, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "If your attack breaks an enemy armor, deal " + PoiseAmt + " poise damage";
    }
}

}