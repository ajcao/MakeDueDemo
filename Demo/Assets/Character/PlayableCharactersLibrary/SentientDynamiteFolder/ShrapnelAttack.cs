using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class ShrapnelAttack : Ability
{
    private int basedamage = 10;
    private int AttackCount = 3;

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
        List<GameObject> CurrentEncounter;   
        
        CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
        for (int i = 0; i < AttackCount; i++)
        {
            if (CurrentEncounter.Count > 0)
            {
                int r = Random.Range(0,CurrentEncounter.Count);
                EnemyCharacter Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
                BattleLogicHandler.AttackDamage(PC, Enem, basedamage + PC.getDamageOutputModifier());
            }
        }
        
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC,null);
    }
    
    public override string GetTooltipString()
    {
        string name = "Shrapnel";
        string s1 = "Deal " + (10 + PC.getDamageOutputModifier()) + " damage to random enemy " + AttackCount + " times";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}