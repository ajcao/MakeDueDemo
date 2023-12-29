using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class NuclearAttackAbility : Ability
{
    private int basedamage = 50;

    public NuclearAttackAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 8;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/DynamiteAbilities/Nuclear") as Sprite;
    }
    
    public override void onCast(Character P)
    {
        
        List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
        foreach (GameObject G in CurrentEncounter)
        {
            EnemyCharacter Enem = G.GetComponent<EnemyCharacter>();
            if (Enem.isAlive() && PC.isAlive())
            {
                    BattleLogicHandler.AttackDamage(PC, Enem, basedamage + PC.getDamageOutputModifier());
            }
            
        }  
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, null);
    }
    
    public override string GetTooltipString()
    {
        string name = "Big Boom";
        string s1 = "Deal " + (basedamage + PC.getDamageOutputModifier()) + " damage to all enemies";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}