using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;

public class GenericEnemy1Behavior : EnemyCharacter
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
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 200;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericEnemy1Icon");
    }
    
    public override void GenerateMoves()
    {
        Character[] Target;
        
        if (BattleSceneHandler.GetTurn() == 1)
        {
            Target = new Character[] {(Character) this};
            
            EnemyApplyBuffMove E = new EnemyApplyBuffMove(this, Target, "GainArmorBuff", 60, null);
            E.SetSpecial();
            Moves.Push(E);
        }
        else
        {
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(3);
            Moves.Push(new EnemyAttackMove(this, 15, Target));
        }
    }
}

