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
        this.CurrentHealth = 100;
        this.MaxHealth = 100;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.AttackStat = 25;
        this.DefenseStat = 25;
        this.Resolve = 0;
        this.MaxResolve = 50;
        
        this.CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/SpikeyRockIcon");
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new DoubleAttackAbility(this));
        this.AbilityPool.Add(new GiveSelfSpikeAbility(this));


    }
    
    public void Start()
    {
        this.InitProtectionList();
    }

}
