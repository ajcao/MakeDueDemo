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
        this.manaCost = 0;
        this.maxCooldown = 0;
        this.currentCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityHeal") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.Restore((PlayableCharacter) E, 10);
    }
    
}


}