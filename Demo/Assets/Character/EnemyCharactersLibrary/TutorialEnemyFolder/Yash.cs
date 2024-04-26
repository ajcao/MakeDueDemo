using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class Yash : EnemyCharacter
{
    //Initalize Stats
    void Awake()
    {
        this.Alive = true;
        this.CurrentHealth = 100;
        this.MaxHealth = 100;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.canPoiseRegenerate = true;
        this.IsStunned = false;
        this.Poise = 100;
        this.MaxPoise = this.Poise;
        this.PoiseRegeneration = this.MaxPoise / 2;
        Moves = new Stack<EnemyMove>();
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericEnemy1Icon");
        
        
    }
    
    private int NoVulnurableMoveTurn = 0;
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        //If the buff was never applied, try to apply
        if (!BuffHandler.CharacterHaveBuff(this,new GainArmorBuff(this, this, 40, null), true))
        {
            Target = new Character[] {(Character) this};
            
            EnemyApplyBuffMove E = new EnemyApplyBuffMove(this, Target, "GainArmorBuff", 40, null);
            Moves.Push(E);
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) <= 0.15f + 0.15f * NoVulnurableMoveTurn)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroesBasedOnBuff(1, new VulnurableBuff(null, null, null, null), true, false);
                Moves.Push(new EnemyApplyBuffMove(this, Target, "VulnurableBuff", null, 4));
                NoVulnurableMoveTurn = 0;
            }
            else
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(3);
                Moves.Push(new EnemyAttackMove(this, 40, Target));
                NoVulnurableMoveTurn+=1;
            }
        }
    }
}