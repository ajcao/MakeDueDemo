using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericPlantBehavior : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 1500;
        this.MaxHealth = 1500;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canPoiseRegenerate = true;
        this.IsStunned = false;
        this.Poise = 300;
        this.MaxPoise = this.Poise;
        this.PoiseRegeneration = this.MaxPoise / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericPlantIcon");
        
        
    }
    
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        //If Saplings have never been summoned, always attempt to spawn all two saplings
        if (EnemyEncounter.getEncounterMember(0) == null && EnemyEncounter.getEncounterMember(1) == null)
        {
            Target = new Character[] {(Character) this};
            (int,int)[] array = new (int,int)[] {(0,0),(0,1)};
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }
        
        GameObject[] Encounter = EnemyEncounter.getEncounter();
        EnemyCharacter E;
        
        //If the first slot is dead or nonexistant revive it
        E = Encounter[1].GetComponent<EnemyCharacter>();
        if (!E.isAlive())
        {
            Target = new Character[] {(Character) E};
            (int,int)[] array = new (int,int)[] {(0,1)};
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }
        
        //If the second slot is dead or nonexistant revive it
        E = Encounter[0].GetComponent<EnemyCharacter>();
        if (!E.isAlive())
        {
            Target = new Character[] {(Character) E};
            (int,int)[] array = new (int,int)[] {(0,0)};
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }
        
        int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(3);
        
        for (int i = 0; i < 2; i++)
        {
            Target = EnemyTargetingLibrary.TargetEnemyType<GenericSaplingBehavior>();

            if (RandomMoveInt[i] == 0)
            {
                List<Buff> appliedBuffs = new List<Buff>();
                foreach (Character C in Target)
                {
                    appliedBuffs.Add(new RetainBuff(C, this, 10, null));
                    appliedBuffs.Add(new GainHPBuff(C, this, 5, null));
                }
                Moves.Push(new EnemyApplyBuffMove(this, Target, appliedBuffs));
            }
            else
            {
                Moves.Push(new EnemyDefendMove(this, 100, Target));
            }
        }
        
        
        
    }
}