using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;

public class SentientDynamiteBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 80;
        this.MaxHealth = 80;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.AttackStat = 10;
        this.DefenseStat = 10;
        this.Resolve = 0;
        this.MaxResolve = 30;
        
        this.CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/SentientDynamiteIcon");
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new AttackEveryoneAbility(this));
    }

}

