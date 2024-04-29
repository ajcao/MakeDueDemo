using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TutorialBoxText
{
    private static (string Name, string[] text) Intro =
    ("Abilities", new String[]
        {
            "MakeDue is a game where you control a party of up to four characters to defeat enemy encounters. You win by lowering the enemy HP while keeping yours above 0",
            "Select a character by hovering over the MOVES button. Every character has abilities. You can read what abilities do by hovering over the ability button",
            "Characters also have a Basic Attack and Defend. Finally all characters have a special Resolve ability",
            "After using a move, it will enter cooldown and be unusable until its cooldown is over. Basic Attack/Defends and the Resolve ability don't have cooldowns",
            "Every character gets one action at the start of the turn. When you are finished your turn, hit the ENDTURN button"
        }
    );

    private static (string Name, string[] text) Armor =
    ("Armor", new String[]
        {
            "Players and Enemies can acquire armor through moves or buffs",
            "Armor directly subtracts the damage dealt by attackers.",
            "When a character's turn begins, all their armor is removed"
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

    public static (string ChapterTitle, string[] Pages)[] Chapters = { Intro, Armor, Resolve, Poise, Buffs, EnemyIntent };

    public static (int x, int y) CurrentCoords = (0,0);


    public static (string, string) GetText(int i)
    //Precondition: i = 1, 0 -1. i shouuld not be any other value
    {
        //Simply give the current page
        if (i == 0)
        {
            return (Chapters[CurrentCoords.x].ChapterTitle, Chapters[CurrentCoords.x].Pages[CurrentCoords.y]);
        }

        if (i == 1)
        {
            //If we are not at the end of the chapter, go to the next page
            if (CurrentCoords.y + i < Chapters[CurrentCoords.x].Pages.Length)
            {
                CurrentCoords = (CurrentCoords.x, CurrentCoords.y + 1);
            }

            //We are at end of the chapter, but we have another chapter
            if (CurrentCoords.y + i == Chapters[CurrentCoords.x].Pages.Length && CurrentCoords.x + i < Chapters.Length)
            {
                CurrentCoords = (CurrentCoords.x + 1, 0);
            }

            //Otherwise, do not change the coordinates at all to avoid out of bounds

        }
        else
        //This case is i == -1
        {
            //If we are not at the start of chapter, go to previous page
            if (CurrentCoords.y + i >= 0)
            {
                CurrentCoords = (CurrentCoords.x, CurrentCoords.y - 1);
            }

            //We are at start of the chapter, but we have a previous chapter
            if (CurrentCoords.y + i < 0 && CurrentCoords.x + i >= 0)
            {
                CurrentCoords = (CurrentCoords.x - 1, Chapters[CurrentCoords.x - 1].Pages.Length - 1);
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
