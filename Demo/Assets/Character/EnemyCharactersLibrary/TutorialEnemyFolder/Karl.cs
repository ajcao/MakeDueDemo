using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class Karl : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 70;
        this.MaxHealth = 150;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canPoiseRegenerate = true;
        this.IsStunned = false;
        this.Poise = 60;
        this.MaxPoise = this.Poise;
        this.PoiseRegeneration = this.MaxPoise / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/KarlIcon");
        
    }
    
    public override void GenerateMoves()
    {
        Character[] Target;
        
        if (this.CurrentHealth < 80)
        {
            Target = new Character[] { (Character)this };
            Moves.Push(new EnemyHealMove(this, 80, Target));
        }
        else
        {
            int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(3);

            if (RandomMoveInt[0] < 2)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                Moves.Push(new EnemyAttackMove(this, 30, Target));
            }
            else
            {
                Target = new Character[] { (Character)this };
                Moves.Push(new EnemyDefendMove(this, 30, Target));
            }

        }
    }

}
