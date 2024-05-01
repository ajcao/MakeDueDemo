using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericSaplingBehavior : EnemyCharacter
{
    private int noBuffTurn;

    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 100;
        this.MaxHealth = 100;
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

        noBuffTurn = Random.Range(0, 2);

        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericSaplingIcon");
        
    }

    public override void GenerateMoves()
    {
        Character[] Target;

        if (Random.Range(0.0f, 1.0f) <= 0.20f + 0.20f * noBuffTurn)
        {
            Target = new Character[] { (Character)this };
            Moves.Push(new EnemyApplyBuffMove(this, Target, "AttackUpBuff", 10, null));
            noBuffTurn = 0;
        }
        else
        {
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(2);
            Moves.Push(new EnemyAttackDefendMove(this, 30, 30, Target));
            noBuffTurn += 1;
        }
    }

}
