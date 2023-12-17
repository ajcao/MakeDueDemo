using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class PalmTreeBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 100;
        this.MaxHealth = 120;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 30;
        this.DefenseStat = 20;
        this.Resolve = 0;
        this.MaxResolve = 40;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/PalmTreeIcon");
        CharacterName = "Potted Palm Tree";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new CoconutMilkAbility(this));
        this.AbilityPool.Add(new CoconutStunAbility(this));
        this.AbilityPool.Add(new CoconutAoeStunAbillity(this));
        this.AbilityPool.Add(new CoconutArmorAbility(this));

        
    }

}

