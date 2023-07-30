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
        currentCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/BurstRNG") as Sprite;
    }
    
    public override void onCast(Character E1)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E1, 20 + PC.getDamageOutputModifier());
        GameObject[] EncounterList = EnemyEncounter.getEncounter();
        int r = Random.Range(0,EncounterList.Length);
        Debug.Log(r);
        EnemyCharacter E2 = EncounterList[r].GetComponent<EnemyCharacter>();
        Debug.Log(E2);
        BattleLogicHandler.PlayerAttack(PC, E2, 10 + PC.getDamageOutputModifier());
    }
    
    public override string GetTooltipString()
    {
        return "Deal 20 damage. Deal 10 additional damage to random enemy";
    }
}


}