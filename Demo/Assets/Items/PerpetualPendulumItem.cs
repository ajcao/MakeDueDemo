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
    
public class PerpetualPendulumItem : GameItem
{
    public PerpetualPendulumItem()
    {
        this.ItemIcon = Resources.Load<Sprite>("ItemImages/PerpetualPendulum");
        this.ItemName = "Perpetual Pendulum";
    }
    
    public override void OnPickup()
    {
        return;
    }
    
    public override void OnApply()
    {
        Buff B = new PerpetualPendulumBuff(this.ItemOwner, this.ItemOwner, null, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        return "Cooldoowns are reduced twice as fast. Reduce Resolve gain by 50% for everyone";
    }
}

}