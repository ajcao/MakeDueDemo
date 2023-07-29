using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;

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
        this.DefenseStat = 50;
        this.Resolve = 0;
        this.MaxResolve = 80;
        
        this.CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/ORBIcon");
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new HealAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
    }

}
