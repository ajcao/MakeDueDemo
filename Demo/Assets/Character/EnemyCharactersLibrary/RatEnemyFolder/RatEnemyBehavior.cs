using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class RatEnemyBehavior : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 250;
        this.MaxHealth = 250;
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
        
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/RatIcon");
        
        //Rats start asleep
        this.Forms = "Sleeping";
        
    }
    
    public override void InitialBuffs()
    {
        Buff B = new SleepingFormBuff(this, this, null, 2);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public int NoDefenseTurn = 0;
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;

        if (this.Forms != "Sleeping")
        {
            //First move is always a defend move
            Target = new Character[] {(Character) this};
            Moves.Push(new EnemyDefendMove(this, 50, Target));
            
            
            int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(2);
            if (Random.Range(0.0f, 1.0f) <= 0.15f + 0.15f * NoDefenseTurn)
            {
                Target = new Character[] {(Character) this};
                Moves.Push(new EnemyApplyBuffMove(this, Target, "DefenseUpBuff", 20, null));
                NoDefenseTurn = 0;
            }
            else
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                Moves.Push(new EnemyAttackDefendMove(this, 30, 10, Target));
                NoDefenseTurn++;
                
            }

        }
        
    }
    
    public override void EnterNewForm(string s)
    {
        Buff B;
        SpriteRenderer currentSprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        switch (s)
        {
            case ("Awake"):
                currentSprite.sprite = Resources.Load<Sprite>("EnemyCharacterImages/RatEnemy");
                B = new EnragedFormBuff(this, this, 120, null);
                BattleLogicHandler.OnBuffApply(B);
                this.Forms = s;
                break;
            case ("Enranged"):
                currentSprite.sprite = Resources.Load<Sprite>("EnemyCharacterImages/EnragedRatEnemy");
                B = new AttackUpBuff(this, this, 20, null);
                BattleLogicHandler.OnBuffApply(B);
                this.Forms = s;
                break;
        }
    }
}