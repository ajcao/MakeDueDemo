using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class HealAbility : Ability
{
    public HealAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityHeal") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.Restore((PlayableCharacter) E, 20);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Restore " + 20 + " health";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}