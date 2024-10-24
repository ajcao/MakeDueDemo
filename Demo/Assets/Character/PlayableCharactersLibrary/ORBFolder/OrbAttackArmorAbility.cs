using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class OrbAttackArmor : Ability
{
    private int armorAmount = 40;
    public OrbAttackArmor(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/OrbAbilities/OrbStealArmor") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        EnemyCharacter E = (EnemyCharacter) C;
        BattleLogicHandler.LowerArmor(E, armorAmount);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Magnetic Pull";
        string s1 = "Reduce enemy's current armor by " + armorAmount;
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}