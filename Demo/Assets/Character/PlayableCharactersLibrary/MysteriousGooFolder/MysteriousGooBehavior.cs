using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class MysteriousGooBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 120;
        this.MaxHealth = 120;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 10;
        this.DefenseStat = 20;
        this.Resolve = 0;
        this.MaxResolve = 80;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/MysteriousGooIcon");
        CharacterName = "Mysterious Goo";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new ApplyVulnurableAbilty(this));
        this.AbilityPool.Add(new AcidAttackAbillity(this));        
        this.AbilityPool.Add(new ShareArmorAbility(this));
        this.AbilityPool.Add(new ArmorAbsorbAbility(this));

        
    }

}

