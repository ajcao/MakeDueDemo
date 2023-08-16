using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class EnemyGivesHPAbilty : Ability
{
    public EnemyGivesHPAbilty(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/ApplyLifeStealImage") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        Buff B = new GiveHPWhenAttackedDebuff(E, this.getPlayableCharacter(), 5, 3);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Debuff enemy. When enemy is attacked, heal player for 5 hp";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}