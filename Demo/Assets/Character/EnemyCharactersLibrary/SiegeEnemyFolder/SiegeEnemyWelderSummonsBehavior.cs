using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class SiegeEnemyWelderSummonsBehavior : EnemyCharacter
{

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
        this.Poise = 150;
        this.MaxPoise = this.Poise;
        this.PoiseRegeneration = this.MaxPoise / 2;
        Moves = new Stack<EnemyMove>();

        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/OrcWelderEnemy.png");
        
    }

    public override void GenerateMoves()
    {
        Character[] Target;

        Target = EnemyTargetingLibrary.TargetEnemyType<SiegeEnemyBehavior>();

        List<Buff> appliedBuffs = new List<Buff>();
        foreach (Character C in Target)
        {
            appliedBuffs.Add(new GainArmorBuff(C, this, 20, null));
            appliedBuffs.Add(new SpikeBuff(C, this, 10, null));
        }
        Moves.Push(new EnemyApplyBuffMove(this, Target, appliedBuffs));
    }
}