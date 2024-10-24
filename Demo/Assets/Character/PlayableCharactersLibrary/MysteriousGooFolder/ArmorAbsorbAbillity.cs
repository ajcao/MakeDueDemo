using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class ArmorAbsorbAbility : Ability
{
    public ArmorAbsorbAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 5;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GooAbillities/ArmorAbsorb") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        int enemyArmor = E.getCurrentArmor();
        BattleLogicHandler.LowerArmor(E, enemyArmor);
                
        BattleLogicHandler.GainArmor(this.PC, this.PC, enemyArmor);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Consume";
        string s1 = "Remove enemy's armor, gain as armor for self";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}