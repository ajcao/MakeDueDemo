using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericEnemy1Behavior : EnemyCharacter
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
        this.Stamina = 200;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericEnemy1Icon");
        
        
    }
    
    private int NoVulnurableMoveTurn = 0;
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        //If the buff was never applied, try to apply
        if (!this.BuffList.Contains(new GainArmorBuff(this, this, 20, null)))
        {
            Target = new Character[] {(Character) this};
            
            EnemyApplyBuffMove E = new EnemyApplyBuffMove(this, Target, "GainArmorBuff", 20, null);
            Moves.Push(E);
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) <= 0.15f + 0.15f * NoVulnurableMoveTurn)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                Moves.Push(new EnemyApplyBuffMove(this, Target, "VulnurableBuff", null, 4));
                NoVulnurableMoveTurn = 0;
            }
            else
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(3);
                Moves.Push(new EnemyAttackMove(this, 20, Target));
                NoVulnurableMoveTurn+=1;
            }
        }
    }
}