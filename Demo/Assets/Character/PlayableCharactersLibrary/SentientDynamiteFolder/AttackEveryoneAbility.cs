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
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
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
        string s1 = "Deal " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage to all enemies";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
}


}