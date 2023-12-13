using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class CoconutMilkAbility : Ability
{
    public CoconutMilkAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/PalmTreeAbilities/CoconutMilk") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        PlayableCharacter P = (PlayableCharacter) C;
        BattleLogicHandler.GainResolve(this.PC, 30);
        BattleLogicHandler.GainResolve(P, 30);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC,C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Coconut Milk";
        string s1 = "Give an both yourself and an ally player 30 resolve";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}