using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TutorialBoxText
{

    private static (string Name, string[] text) Intro =
    ("Intro", new String[]
        {
            "MakeDue is a game where you control a party of up to four characters to defeat enemy encounters.\n" +
            "Combat is turn-based. During your turn, you can use your character's abilities. When your turn is finished, hit the ENDTURN button\n" +
            "You win by reducing the enemy's health to 0 while keeping yours above 0."
        }
    );

    private static (string Name, string[] text) Abilities =
    ("Abilities", new String[]
        {
            "Select a character's abilities by hovering over the MOVES button. You can read what abilities do by hovering over the ability button",
            "Characters also have a Basic Attack and Defend. Finally all characters have a special Resolve ability",
            "After using a move, it will enter cooldown and be unusable until its cooldown is over. Basic Attack/Defends and the Resolve ability don't have cooldowns",
            "Every character gets one action at the start of the turn."
        }
    );

    private static (string Name, string[] text) Armor =
    ("Armor", new String[]
        {
            "Players and Enemies can acquire armor through moves or buffs",
            "Armor directly subtracts the damage dealt by attackers.",
            "When a character's turn begins, all their armor is removed",
            "Armor retain allows a character to keep a proportion of armor for next turn.",
            "All Playable characters have a base armor retain equal to their base defense + defense modifiers"
        }
    );

    private static (string Name, string[] text) Resolve =
    ("Resolve", new String[]
        {
            "Players have a blue Resolve bar. When the blue Resolve bar fills, Players can use their Resolve move to get an extra turn. The Resolve also reduces all cooldowns by 1",
            "Resolve can be acquire in two main ways:\nBlocking damage with armor will gain resolve equal to damage blocked\nBeing at full health will grant 50% of the resolve meter",
            "Resolve can also be acquire through items or abilities"
        }
    );

    private static (string Name, string[] text) Poise =
    ("Poise", new String[]
        {
            "Enemies have a green Poise bar. When the green Poise bar is empty, Enemies are stunned. They also take double damage",
            "Poise can be lowered by dealing damage. Enemies lose poise equal to their damage done to their HP.",
            "An Enemy will try to replensh 50% of their Poise every turn, indicated by a yellow flashing. Deal damage to the poise to interrupt this"
        }
    );

    private static (string Name, string[] text) Buffs =
    ("Buffs", new String[]
        {
            "Buffs can appear below the HP/Armor bar for all chracters. Buffs provide powerful or negative effects",
            "Buffs can be applied by abilities or items. Hover over a buff to read its effects",
        }
    );

    private static (string Name, string[] text) EnemyIntent =
    ("EnemyIntent", new String[]
        {
            "Enemies display their current and future moves above their model.",
            "The bottommost move is their current moves\nEvery subsequent indicator represents a future move.",
            "Stuns from poisebreaks are able to interrupt an enemy's move"
        }
    );

    private static (string Name, string[] text) Outro =
    ("Outro", new String[]
        {
            "There are many enemy encounters and many ways to get stronger. Defeat these two enemies to leave the tutorial"
        }
    );

    public static (string ChapterTitle, string[] Pages)[] Chapters = { Intro, Abilities, Armor, Resolve, Poise, Buffs, EnemyIntent, Outro };

    public static (int x, int y) CurrentCoords = (0,0);


    public static (string, string) GetText(int i)
    //Precondition: i = 1 or -1. i shouuld not be any other value
    {

        if (i == 1)
        {
            //If we are not at the end of the chapter, go to the next page
            if (CurrentCoords.y + i < Chapters[CurrentCoords.x].Pages.Length)
            {
                CurrentCoords.y += 1;
            }
            //We are at end of the chapter, but we have another chapter
            else if (CurrentCoords.y + i == Chapters[CurrentCoords.x].Pages.Length && CurrentCoords.x + i < Chapters.Length)
            {
                CurrentCoords.x += 1;
                CurrentCoords.y = 0;
            }

            //Otherwise, do not change the coordinates at all to avoid out of bounds

        }
        else
        //This case is i == -1
        {
            //If we are not at the start of chapter, go to previous page
            if (CurrentCoords.y + i >= 0)
            {
                CurrentCoords.y -= 1;
            }
            //We are at start of the chapter, but we have a previous chapter
            else if (CurrentCoords.y + i < 0 && CurrentCoords.x + i >= 0)
            {
                //First, set x coord to a lower value
                CurrentCoords.x -= 1;

                //Since x coord is not set, use it to access the current # of pages
                CurrentCoords.y = Chapters[CurrentCoords.x].Pages.Length - 1;
            }

            //Otherwise, do not change the coordinates at all to avoid out of bounds

        }

        //Finallly, return the text at the coordinates
        return (Chapters[CurrentCoords.x].ChapterTitle, Chapters[CurrentCoords.x].Pages[CurrentCoords.y]);
    }

    public static (string, string) GoToChapter(int i)
    {
        if (0 <= i && i < Chapters.Length)
        {
            CurrentCoords = (i, 0);
        }

        return (Chapters[CurrentCoords.x].ChapterTitle, Chapters[CurrentCoords.x].Pages[CurrentCoords.y]);

    }
}
