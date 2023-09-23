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
        BattleLogicHandler.GainArmor(P, PC.getDefenseStat() + PC.getDefenseOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerBasicDefend(PC, (PlayableCharacter) C);
        BattleLogicHandler.PlayerDefend(PC, (PlayableCharacter) C);
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        return "Gain " + (PC.getDefenseStat() + PC.getDefenseOutputModifier()) + " armor";
    }
}


}