using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;

public class LizardLeechBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 30;
        this.MaxHealth = 60;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.AttackStat = 10;
        this.DefenseStat = 10;
        this.Mana = 80;
        this.ManaRegeneration = 0;
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new AttackHealAbility(this));


    }

}

