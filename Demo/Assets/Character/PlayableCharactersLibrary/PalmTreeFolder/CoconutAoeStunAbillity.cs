using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class CoconutAoeStunAbillity : Ability
{
    private int stunAmount = 80;
    public CoconutAoeStunAbillity(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 5;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/PalmTreeAbilities/CoconutAoeStun") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
        foreach (GameObject G in CurrentEncounter)
        {
            EnemyCharacter Enem = G.GetComponent<EnemyCharacter>();
            if (Enem.isAlive() && PC.isAlive())
            {
                BattleLogicHandler.LowerPoise(Enem, stunAmount);
            }
            
        }  
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Uproot";
        string s1 = "Deal " + stunAmount + " poise damage to all enemies";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}