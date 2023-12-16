using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericEnemy2Behavior : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 1000;
        this.MaxHealth = 1000;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 300;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericEnemy2Icon");
        
    }
    
    public override void GenerateMoves()
    {
        Character[] Target;
        
        int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(3);
        
        foreach (int i in RandomMoveInt)
        {
            if (i == 0)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                Moves.Push(new EnemyAttackMove(this, 50, Target));
            }
            
            if (i == 1)
            {
                Target = new Character[] {(Character) this};
                Moves.Push(new EnemyApplyBuffMove(this, Target, "AttackUpBuff", 20, null));
            }
            
            if (i == 2)
            {
                Target = new Character[] {(Character) this};
                Moves.Push(new EnemyDefendMove(this, 50, Target));
            }
        }
    }

}
