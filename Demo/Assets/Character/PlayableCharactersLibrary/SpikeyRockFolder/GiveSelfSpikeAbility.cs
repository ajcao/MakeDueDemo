using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class GiveSelfSpikeAbility : Ability
{
    public GiveSelfSpikeAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GainSpike") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        Buff B = new SpikeBuff(this.getPlayableCharacter(), this.getPlayableCharacter(), 10, null);
        B.onApplication();
    }
    
    public override string GetTooltipString()
    {
        return "Gain buff that deals 10 damage to Enemy Attackers";
    }
    
}


}