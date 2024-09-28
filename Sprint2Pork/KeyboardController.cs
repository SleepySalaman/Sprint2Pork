﻿using Microsoft.Xna.Framework.Input;
using Sprint2Pork.Blocks;
using Sprint2Pork;
using System.Collections.Generic;

public class KeyboardController : IController
{
    private Game1 programGame;
    private Link link;
    private List<Block> listOfBlocks;
    private int currentBlockIndex;

    private KeyboardState previousKeyboardState; // Add field to store the previous keyboard state

    public KeyboardController(Game1 g, Link linkCharacter, List<Block> blocks)
    {
        programGame = g;
        link = linkCharacter;
        listOfBlocks = blocks;
        currentBlockIndex = 0;
        previousKeyboardState = Keyboard.GetState(); // Initialize previousKeyboardState
    }
    public void UpdateLink(Link newLink)
    {
        link = newLink;
    }

    void IController.Update()
    {
        KeyboardState ks = Keyboard.GetState();

        // Link Movement
        if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
        {
            link.LookLeft();
            link.BeMoving();
        }
        else if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
        {
            link.LookRight();
            link.BeMoving();
        }
        else if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W))
        {
            link.LookUp();
            link.BeMoving();
        }
        else if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S))
        {
            link.LookDown();
            link.BeMoving();
        }
        else
        {
            link.BeIdle();
        }

        // Attack or Use Item
        if (ks.IsKeyDown(Keys.Z) || ks.IsKeyDown(Keys.N))
        {
            link.BeAttacking();
        }

        //Link Items
        if (ks.IsKeyDown(Keys.D1))
        {
            link.BeAttacking();
            link.UseItem(1);
        }
        else if (ks.IsKeyDown(Keys.D2))
        {
            link.BeAttacking();
            link.UseItem(2);
        }
        else if (ks.IsKeyDown(Keys.D3))
        {
            link.BeAttacking();
            link.UseItem(3);
        }
        else if (ks.IsKeyDown(Keys.Z) || ks.IsKeyDown(Keys.N))
        {
            link.BeAttacking();
            link.UseItem(4);
        }
        else if (ks.IsKeyDown(Keys.E))
        {
            link.TakeDamage();
        }

        
        if (ks.IsKeyDown(Keys.T) && previousKeyboardState.IsKeyUp(Keys.T))
        {
            currentBlockIndex = (currentBlockIndex + 1) % listOfBlocks.Count;
            programGame.CurrentBlockIndex = currentBlockIndex; 
            programGame.UpdateCurrentBlock(); 
        }
        if (ks.IsKeyDown(Keys.Y) && previousKeyboardState.IsKeyUp(Keys.Y))
        {
            currentBlockIndex = (currentBlockIndex - 1 + listOfBlocks.Count) % listOfBlocks.Count;
            programGame.CurrentBlockIndex = currentBlockIndex; 
            programGame.UpdateCurrentBlock(); 
        }

        if (ks.IsKeyDown(Keys.U) && previousKeyboardState.IsKeyUp(Keys.U))
        {
            programGame.PreviousItem();
        }
        if (ks.IsKeyDown(Keys.I) && previousKeyboardState.IsKeyUp(Keys.I))
        {
            programGame.NextItem();
        }

        if (ks.IsKeyDown(Keys.O) && previousKeyboardState.IsKeyUp(Keys.O))
        {
            programGame.cycleEnemiesBackwards();
        }
        if (ks.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
        {
            programGame.cycleEnemies();
        }

        if (ks.IsKeyDown(Keys.R))
        {
            programGame.ResetGame();
        }
        if (ks.IsKeyDown(Keys.Q))
        {
            programGame.Exit();
        }

        previousKeyboardState = ks;
    }
}
