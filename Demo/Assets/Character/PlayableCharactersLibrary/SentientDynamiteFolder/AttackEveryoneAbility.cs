using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class AttackEveryoneAbility : Ability
{
    public AttackEveryoneAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.EnemyTarget;
        currentCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityAoeAttack") as Sprite;
    }
    
    public override void onCast(Character P)
    {
        foreach (GameObject C in EnemyEncounter.getEncounter())
        {
            EnemyCharacter E = C.GetComponent<EnemyCharacter>();
            BattleLogicHandler.PlayerAttack(PC, E, PC.getAttackStat() + PC.getDamageOutputModifier());
        }
    }
    
    public override string GetTooltipString()
    {
        return "Deal " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage to all enemies";
    }
}


}