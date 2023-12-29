using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class EnemyGivesHPAbilty : Ability
{
    private int duration = 3;
    private int intensity = 10;
    
    public EnemyGivesHPAbilty(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/LizardLeechAbilities/LeechVenom") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        Buff B = new GiveHPWhenAttackedDebuff(E, this.getPlayableCharacter(), intensity, duration);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Leech Venom";
        string s1 = "Debuff enemy for " + duration + " turns. When enemy is attacked, heal player for " + intensity + " hp";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}