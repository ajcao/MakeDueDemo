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
        targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityAoeAttack") as Sprite;
    }
    
    public override void onCast(Character P)
    {
        //Fix to avoid using size
        //Relies on fixing Player Party and Enemy Encounter
        for (int i = 0; i < EnemyEncounter.getEncounterSize(); i++)
        {
            if (i < EnemyEncounter.getEncounterSize())
            {
                EnemyCharacter E = EnemyEncounter.getEncounter()[i].GetComponent<EnemyCharacter>();
                if (E.isAlive() && PC.isAlive())
                {
                    BattleLogicHandler.AttackDamage(PC, E, PC.getAttackStat() + PC.getDamageOutputModifier());
                }
            }
        }
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, null);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Deal " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage to all enemies";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
}


}