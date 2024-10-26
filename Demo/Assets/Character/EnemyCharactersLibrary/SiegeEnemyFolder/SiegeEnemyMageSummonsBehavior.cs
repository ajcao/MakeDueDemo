using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class SiegeEnemyMageSummonsBehavior : EnemyCharacter
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
        this.DefenseOutputModifier = 0;
        this.canPoiseRegenerate = true;
        this.IsStunned = false;
        this.Poise = 200;
        this.MaxPoise = this.Poise;
        this.PoiseRegeneration = this.MaxPoise / 2;
        Moves = new Stack<EnemyMove>();


        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/MageIcon");
        
    }

    public override void GenerateMoves()
    {
        Character[] Target;

        Target = EnemyTargetingLibrary.TargetNRandomHeroes(2);
        Moves.Push(new EnemyAttackMove(this, 80, Target));
    }
}