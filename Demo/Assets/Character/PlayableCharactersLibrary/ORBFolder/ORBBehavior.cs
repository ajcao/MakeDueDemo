using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class ORBBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 30;
        this.MaxHealth = 30;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.AttackStat = 10;
        this.DefenseStat = 30;
        this.Resolve = 0;
        this.MaxResolve = 150;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        this.CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/ORBIcon");
        CharacterName = "ORB";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new HealAbility(this));
        this.AbilityPool.Add(new GiveResolveAbility(this));
    }

}
