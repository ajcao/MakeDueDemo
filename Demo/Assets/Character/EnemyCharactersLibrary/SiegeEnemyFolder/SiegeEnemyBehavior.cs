using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;
using JetBrains.Annotations;

public class SiegeEnemyBehavior : EnemyCharacter
{
    // Initialize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 800;
        this.MaxHealth = 800;
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

        this.MultipleLives = true;

        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericMonsterIcon");
    }

    public bool BigAttackMode = true;
    public int ApplyFrailCounter = 0;
    public bool WasFrailProc = false;
    int SiegeMageSummonsCount = 0;

    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;

        if (EnemyEncounter.getEncounterMember(0) == null && EnemyEncounter.getEncounterMember(1) == null)
        {
            Target = new Character[] { this };
            (int,int)[] array = new (int,int)[] {(0,0),(1,1)};
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }
        
        GameObject[] Encounter = EnemyEncounter.getEncounter();
        EnemyCharacter E;
        
        //If the first slot is dead or nonexistant revive it
        //Warrior slot
        E = Encounter[0].GetComponent<EnemyCharacter>();
        if (!E.isAlive())
        {
            SiegeMageSummonsCount ++;
            Target = new Character[] { E };
            (int,int)[] array = new (int,int)[] {(0,0)};
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }

        //If the second slot is dead or nonexistant revive it
        //Mage slot
        E = Encounter[1].GetComponent<EnemyCharacter>();
        if (!E.isAlive() && SiegeMageSummonsCount < 2)
        {
            SiegeMageSummonsCount ++;
            Target = new Character[] { E };
            (int,int)[] array = new (int,int)[] {(1,1)};
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }

        // int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(3);
        
        // for (int i = 0; i < 2; i++)
        // {
        //     Target = EnemyTargetingLibrary.TargetEnemyType<SiegeEnemyBuilderSummonsBehavior>();

        //     if (RandomMoveInt[i] == 0)
        //     {
        //         List<Buff> appliedBuffs = new List<Buff>();
        //         foreach (Character C in Target)
        //         {
        //             appliedBuffs.Add(new RetainBuff(C, this, 10, null));
        //             appliedBuffs.Add(new GainHPBuff(C, this, 5, null));
        //         }
        //         Moves.Push(new EnemyApplyBuffMove(this, Target, appliedBuffs));
        //     }
        //     else
        //     {
        //         Moves.Push(new EnemyDefendMove(this, 100, Target));
        //     }
        // }

        if (BigAttackMode)
        {
            BigAttackMode = false;
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
            Moves.Push(new EnemyAttackMove(this, 100, Target));
        }
        else
        {
            BigAttackMode = true;
            int[] RandomMoveInt2 = EnemyTargetingLibrary.CreateEvenDistributionToN(5 + ApplyFrailCounter);

            for (int i = 0; i < 3; i++)
            {
                if (RandomMoveInt2[i] < 4)
                {
                    this.RandomAttackOrDefend();
                    continue;
                }

                if (RandomMoveInt2[i] >= 4)
                {
                    if (!WasFrailProc)
                    {
                        this.RandomAttackOrDefend();
                        WasFrailProc = true;
                    }
                    else
                    {
                        this.RandomAttackOrDefend();
                    }
                    continue;
                }
            }

            if (WasFrailProc)
            {
                WasFrailProc = false;
                ApplyFrailCounter = 0;
            }
            else
            {
                ApplyFrailCounter++;
            }
        }
    }

    public void RandomAttackOrDefend()
    {
        Character[] Target;

        int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(2);

        if (RandomMoveInt[0] == 0)
        {
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
            Moves.Push(new EnemyAttackDefendMove(this, 40, 20, Target));
        }
        else
        {
            Target = new Character[] { this };
            Moves.Push(new EnemyDefendMove(this, 40, Target));
        }
    }
}

