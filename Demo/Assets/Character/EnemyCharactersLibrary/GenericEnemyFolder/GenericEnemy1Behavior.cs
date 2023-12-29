using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericEnemy1Behavior : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 600;
        this.MaxHealth = 600;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 200;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericEnemy1Icon");
        
        
    }
    
    private int NoVulnurableMoveTurn = 0;
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        //If the buff was never applied, try to apply
        if (!BuffHandler.CharacterHaveBuff(this,new GainArmorBuff(this, this, 30, null), true))
        {
            Target = new Character[] {(Character) this};
            
            EnemyApplyBuffMove E = new EnemyApplyBuffMove(this, Target, "GainArmorBuff", 30, null);
            Moves.Push(E);
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) <= 0.15f + 0.15f * NoVulnurableMoveTurn)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroesBasedOnBuff(1, new VulnurableBuff(null, null, null, null), true, false);
                Moves.Push(new EnemyApplyBuffMove(this, Target, "VulnurableBuff", null, 4));
                NoVulnurableMoveTurn = 0;
            }
            else
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(3);
                Moves.Push(new EnemyAttackMove(this, 40, Target));
                NoVulnurableMoveTurn+=1;
            }
        }
    }
}