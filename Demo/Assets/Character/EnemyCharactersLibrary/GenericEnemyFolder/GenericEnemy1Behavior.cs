using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;

public class GenericEnemy1Behavior : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 500;
        this.MaxHealth = 500;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 100;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina;
        Moves = new Stack<EnemyMove>();
    }
    
    public override void GenerateMoves()
    {
        Character[] Target = EnemyTargetingLibrary.TargetNRandomHeroes(4);
        Moves.Push(new EnemyAttackMove(this, 15, Target));
    }
}

