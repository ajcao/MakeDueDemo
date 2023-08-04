using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericEnemy2Behavior : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 400;
        this.MaxHealth = 400;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 50;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina;
        Moves = new Stack<EnemyMove>();
    }
    
    public override void GenerateMoves()
    {
        Character[] Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
        Moves.Push(new EnemyAttackMove(this, 30, Target));

        Target = new Character[] {(Character) this};
        Moves.Push(new EnemyApplyBuffMove(this, Target, "AttackUpBuff", 5, null));
        
        Target = new Character[] {(Character) this};
        Moves.Push(new EnemyDefendMove(this, 20, Target));
        
        
    }

}
