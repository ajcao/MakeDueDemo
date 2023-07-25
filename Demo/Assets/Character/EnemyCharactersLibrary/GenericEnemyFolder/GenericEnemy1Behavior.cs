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
        this.CurrentHealth = 700;
        this.MaxHealth = 700;
        this.CurrentArmor = 10;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 70;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina;
        Moves = new Stack<EnemyMove>();
    }
    
    public override void GenerateMoves()
    {
        Character[] Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
        Moves.Push(new EnemyAttackMove(this, 15, Target));
    }
}

