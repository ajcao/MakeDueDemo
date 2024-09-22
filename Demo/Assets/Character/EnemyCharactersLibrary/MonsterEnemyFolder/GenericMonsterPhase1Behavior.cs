using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericMonsterPhase1Behavior : EnemyCharacter
{
    //Initalize Stats
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
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        if (BigAttackMode)
        {
            BigAttackMode = false;
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
            Moves.Push(new EnemyAttackMove(this, 200, Target));
        }
        
        else
        {
            BigAttackMode = true;
            
            //As frail fails to proc, changes of frail goes up
            int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(5 + ApplyFrailCounter);
            
            for(int i = 0; i < 3; i++)
            {
                if (RandomMoveInt[i] < 4)
                {
                    this.RandomAttackOrDefend();
                    continue;
                }
                
                //If frail was already proc, then 50/50 attack or defend. Avoids frail proccing multiple times
                if (RandomMoveInt[i] >= 4)
                {
                    if (!WasFrailProc)
                    {
                        Target = EnemyTargetingLibrary.TargetNRandomHeroes(4);
                        List<Buff> appliedBuffs = new List<Buff>();
                        foreach (Character C in Target)
                        {
                            appliedBuffs.Add(new FrailBuff(C, this, null, 4));
                        }
                        Moves.Push(new EnemyApplyBuffMove(this, Target, appliedBuffs));
                        WasFrailProc = true;
                    }
                    else
                    {
                        this.RandomAttackOrDefend();                   
                    }
                    continue;
                }
            }
            
            //Make applying frail more likely in the future if it didn't proc
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
                Moves.Push(new EnemyAttackDefendMove(this, 100, 20, Target));
        }
        else
        {
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(2);
            Moves.Push(new EnemyAttackDefendMove(this, 50, 20, Target));
        }
    }
    
    public override void EnterNextLife()
    {
        Debug.Log("Prepare for next phase");
        EnemyEncounter.ReplaceEncounterMember(0, 0);
    }
}