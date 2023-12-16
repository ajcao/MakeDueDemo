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
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 400;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericPlantIcon");
        
        
    }
    
    private bool HasSummoned = false;
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        //On the first turn summon all saplings
        if (!HasSummoned)
        {
            Target = new Character[] {(Character) this};
            (int,int)[] array = new (int,int)[] {(0,0),(0,1)};
            Moves.Push(new EnemyReviveMove(this, Target, array));
            HasSummoned = true;
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
        
        //Otherwise have random of three moves
        int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(5);
        
        for (int i = 0; i < 2; i++)
        {
            if (RandomMoveInt[i] < 2)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                Moves.Push(new EnemyAttackMove(this, 80, Target));
            }
            else
            {
                Target = EnemyTargetingLibrary.TargetEnemyType<GenericSaplingBehavior>();
                Moves.Push(new EnemyDefendMove(this, 60, Target));
            }
        }
        
        
        
    }
}