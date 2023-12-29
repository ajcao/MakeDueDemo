using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class MushroomHealAbility : Ability
{
    
    private int healamount = 20;
    public MushroomHealAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/MushroomAbilities/MushroomHeal") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        BattleLogicHandler.GainHealth((PlayableCharacter) C, healamount);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Shroom Berry";
        string s1 = "Restore " + healamount + " health to an ally player";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}