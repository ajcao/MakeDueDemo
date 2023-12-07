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
        this.CurrentHealth = 80;
        this.MaxHealth = 80;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 50;
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
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;

        if (this.Forms != "Sleeping")
        {
            int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(6);
            for (int j = 0; j < 2; j++)
            {
                int i = RandomMoveInt[j];

                if (i < 4)
                {
                        Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                        Moves.Push(new EnemyAttackMove(this, 20, Target));
                }

                if (i >= 4)
                {
                        Target = EnemyTargetingLibrary.TargetNRandomHeroes(2);
                        Moves.Push(new EnemyAttackMove(this, 10, Target));
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
                B = new EnragedFormBuff(this, this, 40, null);
                BattleLogicHandler.OnBuffApply(B);
                this.Forms = s;
                break;
            case ("Enranged"):
                currentSprite.sprite = Resources.Load<Sprite>("EnemyCharacterImages/EnragedRatEnemy");
                B = new AttackUpBuff(this, this, 10, null);
                BattleLogicHandler.OnBuffApply(B);
                this.Forms = s;
                break;
        }
    }
}