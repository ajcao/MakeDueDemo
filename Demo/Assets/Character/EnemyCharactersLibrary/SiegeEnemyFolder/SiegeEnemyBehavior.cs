using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.U2D.Animation;

public class SiegeEnemyBehavior : EnemyCharacter
{
    // Initialize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 1000;
        this.MaxHealth = 1000;
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

        this.Forms = "Normal";

        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericMonsterIcon");
    }

    public override void InitialBuffs()
    {
        Buff B = new DamagedFormBuff(this, this, 500, null);
        BattleLogicHandler.OnBuffApply(B);
    }

    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;

        if (AttemptToSummonUnits())
        {
            //Summon move, do not do anything else
            return;
        }


        AttackCycle();
    }

    //Did summon?
    private bool AttemptToSummonUnits()
    {
        Character[] Target;
        GameObject[] Encounter = EnemyEncounter.getEncounter();
        EnemyCharacter E;

        //Summon First Unit if possible
        if (EnemyEncounter.getEncounterMember(0) == null || !Encounter[0].GetComponent<EnemyCharacter>().isAlive())
        {
            Target = new Character[] { this };
            (int, int)[] array = new (int, int)[] { (0, 0) };
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return true;
        }


        if (this.Forms == "Normal")
        {
            if (!Encounter[1].GetComponent<EnemyCharacter>().isAlive())
            {
                Target = new Character[] { this };
                (int, int)[] array = new (int, int)[] { (1, 1) };
                Moves.Push(new EnemyReviveMove(this, Target, array));
                return true;
            }

        }
        else //this.Forms == Damaged
        {
            if ( !Encounter[1].GetComponent<EnemyCharacter>().isAlive() || Encounter[1].GetComponent<EnemyCharacter>().GetType() == typeof(SiegeEnemyBuilderSummonsBehavior) )
            {
                Target = new Character[] { this };
                (int, int)[] array = new (int, int)[] { (2, 1) };
                Moves.Push(new EnemyReviveMove(this, Target, array));
                return true;
            }
        }

        return false;
    }

    int attackCycleValue = 0;
    private void AttackCycle()
    {
        Character[] Target;
        if (attackCycleValue < 2)
        {
            int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(2);

            if (RandomMoveInt[0] == 0)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                Moves.Push(new EnemyAttackMove(this, 150, Target));
            }
            else
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(3);
                Moves.Push(new EnemyAttackMove(this, 50, Target));
            }

            attackCycleValue++;
        }
        else
        {
            List<Buff> appliedBuffs;

            Target = EnemyTargetingLibrary.TargetNRandomHeroesBasedOnBuff(1, new VulnurableBuff(null, null, null, null), false, false);
            appliedBuffs = new List<Buff>();
            appliedBuffs.Add(new VulnurableBuff(Target[0].GetComponent<PlayableCharacter>(), this, null, 5));
            appliedBuffs.Add(new FrailBuff(Target[0].GetComponent<PlayableCharacter>(), this, null, 5));
            appliedBuffs.Add(new WeakBuff(Target[0].GetComponent<PlayableCharacter>(), this, null, 5));
            Moves.Push(new EnemyApplyBuffMove(this, Target, appliedBuffs));
            attackCycleValue = 0;
        }
    }



    public override void EnterNewForm(string s)
    {
        SpriteRenderer currentSprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        switch (s)
        {
            case ("Damaged"):
                currentSprite.sprite = Resources.Load<Sprite>("EnemyCharacterImages/SiegeUnitDamaged");
                this.Forms = s;
                break;
        }
    }

}

