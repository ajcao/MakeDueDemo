using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using BuffUtil;
using JetBrains.Annotations;

public class SiegeEnemyBehavior : EnemyCharacter
{
    // Initialize Stats
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

        if (EnemyEncounter.getEncounterMember(0) == null && EnemyEncounter.getEncounterMember(1) == null)
        {
            Target = new Character[] { this };
            (int, int)[] array = new (int, int)[] { (0, 0), (1, 1) };
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }

        GameObject[] Encounter = EnemyEncounter.getEncounter();
        EnemyCharacter E;

        //If the first slot is dead or nonexistant revive it
        //Warrior slot
        E = Encounter[0].GetComponent<EnemyCharacter>();
        if (!E.isAlive())
        {
            Target = new Character[] { E };
            (int, int)[] array = new (int, int)[] { (0, 0) };
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }

        //If the second slot is dead or nonexistant revive it
        //Mage/Builder slot
        E = Encounter[1].GetComponent<EnemyCharacter>();
        if (!E.isAlive())
        {
            Target = new Character[] { E };
            (int, int)[] array = new (int, int)[] { (2, 1) };
            Moves.Push(new EnemyReviveMove(this, Target, array));
            return;
        }




        BigAttackMode = false;
        Target = EnemyTargetingLibrary.TargetNRandomHeroes(1);
        Moves.Push(new EnemyAttackMove(this, 100, Target));
    }

}

