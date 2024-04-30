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
        this.CurrentHealth = 600;
        this.MaxHealth = 600;
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
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericMonsterIcon");
        
        
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
            Moves.Push(new EnemyAttackMove(this, 250, Target));
        }
        else
        {
            
            AttackMode = true;
            
            int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(2);
            
            if (RandomMoveInt[0] == 0)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(4);
                Moves.Push(new EnemyApplyBuffMove(this, Target, "VulnurableBuff", null, 2));
            }
            else
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(2);
                Moves.Push(new EnemyAttackDefendMove(this, 80, 20, Target));
            }
        }

        
        
        
    }
}