using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class SiegeEnemySpearSummonsBehavior : EnemyCharacter
{
    private int noBuffTurn;

    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 300;
        this.MaxHealth = 300;
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

        noBuffTurn = Random.Range(0, 2);

        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/OrcWarriorEnemy.png");
        
    }

    public override void GenerateMoves()
    {
        Character[] Target;
        List<Buff> appliedBuffs;

        //If the buff was never applied, try to apply
        if (!BuffHandler.CharacterHaveBuff(this, new PermaAttackScaleBuff(this, this, 5, null), true))
        {
            Target = new Character[] { (Character)this };
            appliedBuffs = new List<Buff>();
            appliedBuffs.Add(new PermaAttackScaleBuff(this, this, 5, null));
            EnemyApplyBuffMove E = new EnemyApplyBuffMove(this, Target, appliedBuffs);
            Moves.Push(E);
        }
        else
        {
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
            Moves.Push(new EnemyAttackMove(this, 30, Target));
        }
    }
}