using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class ShrapnelAttack : Ability
{
    public ShrapnelAttack(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/DynamiteAbilities/ShrapnelAttack") as Sprite;
    }
    
    public override void onCast(Character InputE)
    {        
        
        List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
        int r = Random.Range(0,CurrentEncounter.Count);
        EnemyCharacter Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
        BattleLogicHandler.AttackDamage(PC, Enem, 10 + PC.getDamageOutputModifier());
    
        CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
        r = Random.Range(0,CurrentEncounter.Count);
        Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
        BattleLogicHandler.AttackDamage(PC, Enem, 10 + PC.getDamageOutputModifier());
        
        CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
        r = Random.Range(0,CurrentEncounter.Count);
        Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
        BattleLogicHandler.AttackDamage(PC, Enem, 10 + PC.getDamageOutputModifier());
        
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC,null);
    }
    
    public override string GetTooltipString()
    {
        string name = "Shrapnel";
        string s1 = "Deal " + (10 + PC.getDamageOutputModifier()) + " damage to random enemy three times";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}