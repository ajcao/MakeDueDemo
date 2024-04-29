using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class Yash : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 200;
        this.MaxHealth = 200;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canPoiseRegenerate = true;
        this.IsStunned = false;
        this.Poise = 100;
        this.MaxPoise = this.Poise;
        this.PoiseRegeneration = this.MaxPoise / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/YahsIcon");
        
        
    }

    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);

        Moves.Push(new EnemyAttackMove(this, 50, Target));
        Moves.Push(new EnemyAttackMove(this, 10, Target));
        Moves.Push(new EnemyAttackMove(this, 10, Target));
    }
}