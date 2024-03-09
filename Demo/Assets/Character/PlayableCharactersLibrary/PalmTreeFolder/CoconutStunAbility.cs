using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class CoconutStunAbility : Ability
{
    
    private int stunAmount = 150;
    
    public CoconutStunAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 6;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/PalmTreeAbilities/CoconutStun") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.LowerPoise((EnemyCharacter) E, stunAmount);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Coconut Drop";
        string s1 = "Deal " + stunAmount + " poise damage";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}