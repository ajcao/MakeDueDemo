using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class SiegeEnemyBuilderSummonsBehavior : EnemyCharacter
{
    private int noBuffTurn;

    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 1;
        this.MaxHealth = 1;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canPoiseRegenerate = true;
        this.IsStunned = false;
        this.Poise = 50;
        this.MaxPoise = this.Poise;
        this.PoiseRegeneration = this.MaxPoise / 2;
        Moves = new Stack<EnemyMove>();

        noBuffTurn = Random.Range(0, 2);

        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/OrcMageEnemy.png");
        
    }

    public override void GenerateMoves()
    {
        Character[] Target;

        Target = EnemyTargetingLibrary.TargetEnemyType<SiegeEnemyBehavior>();

        Moves.Push(new EnemyDefendMove(this, 60, Target));
    }
}