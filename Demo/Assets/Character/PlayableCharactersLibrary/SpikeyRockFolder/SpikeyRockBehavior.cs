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
        this.CurrentHealth = 140;
        this.MaxHealth = 140;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 30;
        this.DefenseStat = 30;
        this.Resolve = 0;
        this.MaxResolve = 100;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);

        this.CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/SpikeyRockIcon");
        CharacterName = "Spikey Rock";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new AttackDefendAbility(this));
        this.AbilityPool.Add(new GiveSelfSpikeAbility(this));        
        this.AbilityPool.Add(new ArmorSpikeAbility(this));
        this.AbilityPool.Add(new ArmorRetainAbility(this));


    }

}
