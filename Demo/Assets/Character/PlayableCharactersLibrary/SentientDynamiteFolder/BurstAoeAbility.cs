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
        targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/BurstRNG") as Sprite;
    }
    
    public override void onCast(Character InputE)
    {        
        //Fix to avoid using size
        //Relies on fixing Player Party and Enemy Encounter
        //Fix this, gets issues with null spot
        
        int r = Random.Range(0,EnemyEncounter.GetLivingEncounterMembers().Count);
        EnemyCharacter E = EnemyEncounter.getEncounterMember(r).GetComponent<EnemyCharacter>();
        BattleLogicHandler.AttackDamage(PC, E, 10 + PC.getDamageOutputModifier());
        
        r = Random.Range(0,EnemyEncounter.GetLivingEncounterMembers().Count);
        E = EnemyEncounter.getEncounterMember(r).GetComponent<EnemyCharacter>();
        BattleLogicHandler.AttackDamage(PC, E, 10 + PC.getDamageOutputModifier());
    
    
        r = Random.Range(0,EnemyEncounter.GetLivingEncounterMembers().Count);
        E = EnemyEncounter.getEncounterMember(r).GetComponent<EnemyCharacter>();
        BattleLogicHandler.AttackDamage(PC, E, 10 + PC.getDamageOutputModifier());
        
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC,null);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Deal 10 additional damage to random enemy three times";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
}


}