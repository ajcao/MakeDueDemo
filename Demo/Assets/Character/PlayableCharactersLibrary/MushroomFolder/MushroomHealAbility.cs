using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class MushroomHealAbility : Ability
{
    public MushroomHealAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/MushroomHeal") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        BattleLogicHandler.GainHealth((PlayableCharacter) C, 10);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Restore " + 10 + " health";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}