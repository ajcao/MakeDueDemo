using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;

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
        this.Poise = 200;
        this.PoiseRegeneration = -1;
        Moves = new Queue<EnemyMove>();
    }
    
    public override void GenerateMoves()
    {
        Character[] Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
        Moves.Enqueue(new EnemyAttackMove(this, 5, Target));
        
        Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
        Moves.Enqueue(new EnemyAttackMove(this, 5, Target));
        
        Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
        Moves.Enqueue(new EnemyAttackMove(this, 30, Target));
        
    }

}
