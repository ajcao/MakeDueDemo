using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class DefendAbility : Ability
{
    public DefendAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.PlayerTarget;
        currentCooldown = 0;
        maxCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/DefendIcon");
    }
    
    public override void onCast(Character P)
    {
        BattleLogicHandler.PlayerDefend(PC, (PlayableCharacter) P, PC.getDefenseStat());
    }
    
    public override string GetTooltipString()
    {
        return "Gain " + PC.getDefenseStat() + " armor";
    }
}


}