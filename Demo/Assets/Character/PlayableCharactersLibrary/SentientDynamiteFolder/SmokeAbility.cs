using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{
  
  
public class SmokeAbility : Ability
{
    public SmokeAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/DynamiteAbilities/SmokeScreen") as Sprite;
    }
    
    public override void onCast(Character P)
    {
        
        List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
        foreach (GameObject G in CurrentEncounter)
        {
            EnemyCharacter Enem = G.GetComponent<EnemyCharacter>();
            if (Enem.isAlive() && PC.isAlive())
            {
                Buff B = new WeakBuff(Enem, this.getPlayableCharacter(), null, 3);
                BattleLogicHandler.OnBuffApply(B);
            }
            
        }  
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, null);
    }
    
    public override string GetTooltipString()
    {
        string name = "Smokescreen";
        string s1 = "Apply 1 weak to all enemies. Weak reduces damage by 50%";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}