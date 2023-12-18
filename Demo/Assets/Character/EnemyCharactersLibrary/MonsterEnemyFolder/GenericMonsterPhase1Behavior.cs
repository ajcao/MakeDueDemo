using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;

public class GenericMonsterPhase1Behavior : EnemyCharacter
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
        this.canStaminaRegenerate = true;
        this.IsStunned = false;
        this.Stamina = 200;
        this.MaxStamina = this.Stamina;
        this.StaminaRegeneration = this.MaxStamina / 2;
        Moves = new Stack<EnemyMove>();
        
        this.MultipleLives = true;
        
        this.CharacterIcon = Resources.Load<Sprite>("EnemyCharacterImages/GenericMonsterIcon");
        
        
    }
    
    public bool BigAttackMode = true;
    
    public int ApplyFrailCounter = 0;
    public bool WasFrailProc = false;
    
    public override void GenerateMoves()
    {
        Debug.Log("Generating moves");
        Character[] Target;
        
        if (BigAttackMode)
        {
            BigAttackMode = false;
            Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
            Moves.Push(new EnemyAttackMove(this, 100, Target));
        }
        
        else
        {
        
            BigAttackMode = true;
                    
            int[] RandomMoveInt = EnemyTargetingLibrary.CreateEvenDistributionToN(5 + ApplyFrailCounter);
            
            for(int i = 0; i < 3; i++)
            {
                //Attack
                if (RandomMoveInt[i] < 2)
                {
                    Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
                    Moves.Push(new EnemyAttackDefendMove(this, 40, 20, Target));
                    continue;
                }
                
                //Defend
                if (RandomMoveInt[i] < 4)
                {
                    Target = new Character[] {(Character) this};
                    Moves.Push(new EnemyDefendMove(this, 40, Target));
                    continue;
                }
                
                //If frail was already proc, then simply defend
                if (RandomMoveInt[i] >= 4)
                {
                    if (!WasFrailProc)
                    {
                        Target = EnemyTargetingLibrary.TargetNRandomHeroes(4);
                        Moves.Push(new EnemyApplyBuffMove(this, Target, "FrailBuff", null, 4));
                        WasFrailProc = true;
                    }
                    else
                    {
                        Target = new Character[] {(Character) this};
                        Moves.Push(new EnemyDefendMove(this, 40, Target));                        
                    }
                    continue;
                    
                }
            }
            
            //Make applying frail more likely in the future
            if (WasFrailProc)
            {
                WasFrailProc = false;
                ApplyFrailCounter = 0;
            }
            else
            {
                ApplyFrailCounter++;
            }
        }
        
    }
    
    public override void EnterNextLife()
    {
        Debug.Log("Prepare for next phase");
        EnemyEncounter.ReplaceEncounterMember(0, 0);
    }
}