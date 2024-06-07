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
        this.CurrentHealth = 800;
        this.MaxHealth = 800;
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
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericEnemy1Icon");
        
        
    }
    
    private int NoVulnurableMoveTurn = 0;
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        List<Buff> appliedBuffs;
        
        //If the buff was never applied, try to apply
        if (!BuffHandler.CharacterHaveBuff(this,new GainArmorBuff(this, this, 40, null), true))
        {
            Target = new Character[] {(Character) this};
            appliedBuffs = new List<Buff>();
            appliedBuffs.Add(new GainArmorBuff(this, this, 40, null));
            EnemyApplyBuffMove E = new EnemyApplyBuffMove(this, Target, appliedBuffs);
            Moves.Push(E);
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) <= 0.15f + 0.15f * NoVulnurableMoveTurn)
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroesBasedOnBuff(2, new VulnurableBuff(null, null, null, null), false, false);
                appliedBuffs = new List<Buff>();
                foreach (Character C in Target)
                {
                    appliedBuffs.Add(new VulnurableBuff(C, this, null, 5));
                }
                Moves.Push(new EnemyApplyBuffMove(this, Target, appliedBuffs));
                NoVulnurableMoveTurn = 0;
            }
            else
            {
                Target = EnemyTargetingLibrary.TargetNRandomHeroes(3);
                Moves.Push(new EnemyAttackMove(this, 50, Target));
                NoVulnurableMoveTurn+=1;
            }
        }
    }
}