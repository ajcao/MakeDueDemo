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
        this.maxCooldown = 8;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/RolyPolyAbilities/RollAttack") as Sprite;
    }
    
    public override void onCast(Character P)
    {
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) P, 90 + PC.getDamageOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Roll Attack";
        string s1 = "Deal " + (90 + PC.getDamageOutputModifier()) + " damage";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}