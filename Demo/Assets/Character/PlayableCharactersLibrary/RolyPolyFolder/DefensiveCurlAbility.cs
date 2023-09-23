using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class DefensiveCurlAbility : Ability
{
    public DefensiveCurlAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.NoTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/DefensiveCurl") as Sprite;
    }
    
    public override void onCast(Character PI)
    {
        //Fix to avoid using size
        //Relies on fixing Player Party and Enemy Encounter
        foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
        {
            PlayableCharacter C = G.GetComponent<PlayableCharacter>();
            BattleLogicHandler.GainArmor(C, 10);
        }
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerDefend(PC,null);
        BattleLogicHandler.PlayerSkill(PC,null);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Give everyone 10 armor";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
}


}