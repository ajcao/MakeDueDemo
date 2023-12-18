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
    
public class MosquitoHeadItem : GameItem
{
    public MosquitoHeadItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/MosquitoHead");
        this.ItemName = "Mosquito Head";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new MosquitoHeadItemBuff(this.ItemOwner, this.ItemOwner, 10, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Whenever you deal damage via attacks, gain 10 hp";
    }
}

}