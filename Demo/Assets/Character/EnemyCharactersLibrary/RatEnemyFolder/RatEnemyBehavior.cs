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
        this.CurrentHealth = 400;
        this.MaxHealth = 400;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 150;
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
            
            Target = new Character[] {(Character) this};
            Moves.Push(new EnemyDefendMove(this, 60, Target));
            
            //When enranged, only attack
            if (this.Forms == "Enranged")
            {
                    Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                    Moves.Push(new EnemyAttackDefendMove(this, 80, 20, Target));
                    NoDefenseTurn++;
            }
            else //Enemy is attacking normally
            {

                if (Random.Range(0.0f, 1.0f) <= 0.3f + 0.3f * NoDefenseTurn)
                {
                    Target = new Character[] {(Character) this};
                    Moves.Push(new EnemyApplyBuffMove(this, Target, "DefenseUpBuff", 40, null));
                    NoDefenseTurn = -1;
                }
                else
                {
                    Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                    Moves.Push(new EnemyAttackDefendMove(this, 80, 20, Target));
                    NoDefenseTurn++;
                }
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
                B = new EnragedFormBuff(this, this, 200, null);
                BattleLogicHandler.OnBuffApply(B);
                this.Forms = s;
                break;
            case ("Enranged"):
                currentSprite.sprite = Resources.Load<Sprite>("EnemyCharacterImages/EnragedRatEnemy");
                B = new RatLifeStealBuff(this, this, null, null);
                BattleLogicHandler.OnBuffApply(B);
                this.Forms = s;
                break;
        }
    }
}