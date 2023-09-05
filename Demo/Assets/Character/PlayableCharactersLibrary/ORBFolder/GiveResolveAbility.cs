using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class GiveResolveAbility : Ability
{
    public GiveResolveAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericGiveResolve") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        PlayableCharacter P = (PlayableCharacter) E;
        P.setResolve(P.getResolve() + 60);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Gain " + 60 + " resolve";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}