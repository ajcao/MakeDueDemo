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
        this.CurrentHealth = 50;
        this.MaxHealth = 50;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 50;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CanRevive = true;
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericMonsterIcon");
        
        
    }
    
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        
  
        
        //Otherwise have random of three moves
        int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(5);
        
        foreach (int i in RandomMoveInt)
        {
            if (i == 0)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                Moves.Push(new EnemyAttackMove(this, 80, Target));
            }
            else
            
            if (i == 2)
            {
                Target = new Character[] {(Character) this};
                Moves.Push(new EnemyDefendMove(this, 40, Target));
            }
        }
        
    }
    
    public override void PrepareNextPhase()
    {
        Debug.Log("Prepare for next phase");
        Character[] Target = new Character[] {(Character) this};
        (int,int)[] array = new (int,int)[] {(0,0)};
        Moves.Push(new EnemyReviveMove(this, Target, array));
    }
}