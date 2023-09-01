using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class LizardLeechBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 60;
        this.MaxHealth = 60;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.AttackStat = 20;
        this.DefenseStat = 10;
        this.Resolve = 0;
        this.MaxResolve = 40;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/LizardleechIcon");
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new AttackHealAbility(this));
        this.AbilityPool.Add(new EnemyGivesHPAbilty(this));

        
    }
    
    public void Start()
    {
        this.InitProtectionList();
    }

}

