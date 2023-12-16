using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{
  
  
public class HPForATKAbility : Ability
{
    public HPForATKAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 5;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/LizardLeechAbilities/Frenzy") as Sprite;
    }
    
    public override void onCast(Character PI)
    {
        BattleLogicHandler.DirectlyLoseHP((Character) this.getPlayableCharacter(), 30);
        
        Buff B = new AttackUpBuff(this.getPlayableCharacter(), this.getPlayableCharacter(), 5, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC,null);
    }
    
    public override string GetTooltipString()
    {
        string name = "Frenzy";
        string s1 = "Lose 30 hp. Gain 5 strength";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}