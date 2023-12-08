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
        
        List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
        foreach (GameObject G in CurrentEncounter)
        {
            EnemyCharacter Enem = G.GetComponent<EnemyCharacter>();
            if (Enem.isAlive() && PC.isAlive())
            {
                    BattleLogicHandler.AttackDamage(PC, Enem, PC.getAttackStat() + PC.getDamageOutputModifier());
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