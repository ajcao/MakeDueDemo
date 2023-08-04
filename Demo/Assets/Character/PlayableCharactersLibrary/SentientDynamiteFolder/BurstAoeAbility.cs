using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class BurstAoeAbility : Ability
{
    public BurstAoeAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/BurstRNG") as Sprite;
    }
    
    public override void onCast(Character E1)
    {        
        //Fix to avoid using size
        //Relies on fixing Player Party and Enemy Encounter
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E1, 20 + PC.getDamageOutputModifier());
        if (EnemyEncounter.getEncounterSize() > 0)
        {
            int r = Random.Range(0,EnemyEncounter.getEncounterSize());
            EnemyCharacter E2 = EnemyEncounter.getEncounterMember(r).GetComponent<EnemyCharacter>();
            BattleLogicHandler.PlayerAttack(PC, E2, 10 + PC.getDamageOutputModifier());
        }
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Deal 20 damage. Deal 10 additional damage to random enemy";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
}


}