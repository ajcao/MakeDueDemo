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
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/LizardLeechAbilities/LeechVenom") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        Buff B = new GiveHPWhenAttackedDebuff(E, this.getPlayableCharacter(), 10, 3);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Leech Venom";
        string s1 = "Debuff enemy for 3 turns. When enemy is attacked, heal player for 10 hp";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}