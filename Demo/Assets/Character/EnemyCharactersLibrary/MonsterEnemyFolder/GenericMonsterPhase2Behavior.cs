using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericMonsterPhase2Behavior : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 500;
        this.MaxHealth = 500;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 100;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericMonsterIcon");
        
        
    }
    
    public override void InitialBuffs()
    {
        Buff B = new AttackUpBuff(this, this, 20, null);
        BattleLogicHandler.OnBuffApply(B);
        
        B = new DefenseUpBuff(this, this, 20, null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    
    public bool AttackMode = true;
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        
        if (AttackMode)
        {
            AttackMode = false;
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
            Moves.Push(new EnemyAttackMove(this, 120, Target));
        }
        else
        {
            
            AttackMode = true;
            
            int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(2);
            
            if (RandomMoveInt[0] == 0)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(4);
                Moves.Push(new EnemyApplyBuffMove(this, Target, "Vulnurable", null, 2));
            }
            else
            {
                Target = new Character[] {(Character) this};
                Moves.Push(new EnemyDefendMove(this, 40, Target));        
            }
        }

        
        
        
    }
}