using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class DefensiveCurlAbility : Ability
{
    private int basearmor = 25;

    public DefensiveCurlAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/RolyPolyAbilities/ChitinShield") as Sprite;
    }
    
    public override void onCast(Character PI)
    {
        //Fix to avoid using size
        //Relies on fixing Player Party and Enemy Encounter
        foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
        {
            PlayableCharacter C = G.GetComponent<PlayableCharacter>();
            BattleLogicHandler.GainArmor(PC, C, basearmor + PC.getDefenseOutputModifier());
        }
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerDefend(PC,null);
        BattleLogicHandler.PlayerSkill(PC,null);
    }
    
    public override string GetTooltipString()
    {
        string name = "Chitin Scale";
        string s1 = "Defend all allies for " + (basearmor + PC.getDefenseOutputModifier()) + " armor";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}