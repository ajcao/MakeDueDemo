using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericSaplingBehavior : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 150;
        this.MaxHealth = 150;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 80;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericSaplingIcon");
        
    }
    
    public override void GenerateMoves()
    {
        Character[] Target;
        
        int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(6);
        
        for (int j = 0; j < 2; j++)
        {
            int i = RandomMoveInt[j];
            
            if (i < 4)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                Moves.Push(new EnemyAttackDefendMove(this, 40, 40, Target));
            }
            
            if (i >= 4)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(2);
                Moves.Push(new EnemyAttackDefendMove(this, 20, 40, Target));
            }
        }
    }

}
