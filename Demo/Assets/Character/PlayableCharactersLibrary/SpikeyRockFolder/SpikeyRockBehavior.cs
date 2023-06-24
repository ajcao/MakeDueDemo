using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;

public class SpikeyRockBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 30;
        this.MaxHealth = 100;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.AttackStat = 20;
        this.DefenseStat = 10;
        this.Mana = 40;
        this.ManaRegeneration = 0;
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new DoubleAttackAbility(this));


    }

}
