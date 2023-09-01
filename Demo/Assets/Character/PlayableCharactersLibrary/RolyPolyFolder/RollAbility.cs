using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class RollAbility : Ability
{
    public RollAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/RollAttack") as Sprite;
    }
    
    public override void onCast(Character P)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) P, 50);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Deal 50 damage";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
}


}