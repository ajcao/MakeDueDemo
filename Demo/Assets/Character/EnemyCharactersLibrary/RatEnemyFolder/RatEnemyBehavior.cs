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
        this.CurrentHealth = 500;
        this.MaxHealth = 500;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canPoiseRegenerate = true;
        this.IsStunned = false;
        this.Poise = 200;
        this.MaxPoise = this.Poise;
        this.PoiseRegeneration = this.MaxPoise / 2;
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
            
            //Attack only when enraged
            if (this.Forms == "Enraged")
            {
                    Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                    Moves.Push(new EnemyAttackDefendMove(this, 120, 60, Target));
                    NoDefenseTurn++;
            }
            else //Enemy is attacking normally
            {

                if (Random.Range(0.0f, 1.0f) <= 0.3f + 0.3f * NoDefenseTurn)
                {
                    Target = new Character[] {(Character) this};
                    List<Buff> appliedBuffs = new List<Buff>();
                    appliedBuffs.Add(new DefenseUpBuff(this, this, 40, null));
                    Moves.Push(new EnemyApplyBuffMove(this, Target, appliedBuffs));
                    NoDefenseTurn = -1;
                }
                else
                {
                    Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                    Moves.Push(new EnemyAttackDefendMove(this, 80, 20, Target));
                    NoDefenseTurn++;
                }

                Target = new Character[] { (Character)this };
                Moves.Push(new EnemyDefendMove(this, 60, Target));
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
            case ("Enraged"):
                currentSprite.sprite = Resources.Load<Sprite>("EnemyCharacterImages/EnragedRatEnemy");
                B = new RatLifeStealBuff(this, this, null, null);
                BattleLogicHandler.OnBuffApply(B);
                this.Forms = s;
                break;
        }
    }
}