using Microsoft.Xna.Framework.Input;
using Sprint2Pork.Blocks;
using Sprint2Pork;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class KeyboardController : IController
{
    private Game1 programGame;
    private Link link;
    private List<Block> listOfBlocks;
    private int currentBlockIndex;

    private KeyboardState previousKeyboardState;

    // Add fields to control damage effect
    private int damageEffectCounter;
    private bool isTakingDamage;
    private int flashRate; // Add a variable to control the flash speed

    public KeyboardController(Game1 g, Link linkCharacter, List<Block> blocks)
    {
        programGame = g;
        link = linkCharacter;
        listOfBlocks = blocks;
        currentBlockIndex = 0;
        previousKeyboardState = Keyboard.GetState();

        // Initialize damage effect fields
        damageEffectCounter = 0;
        isTakingDamage = false;
        flashRate = 5; // Flash every 5 frames
    }

    public void UpdateLink(Link newLink)
    {
        link = newLink;
    }

    void IController.Update()
    {
        KeyboardState ks = Keyboard.GetState();
        ILinkItems linkItem = link.linkItem;

        if (isTakingDamage)
        {
            if (damageEffectCounter % flashRate == 0)
            {
                if (damageEffectCounter / flashRate % 2 == 0)
                {
                    link.TakeDamage();
                }
                else
                {
                    link.BeIdle();
                }
            }

            damageEffectCounter++;
            if (damageEffectCounter >= flashRate * 10)
            {
                isTakingDamage = false;
                damageEffectCounter = 0;
            }

            previousKeyboardState = ks;
            return;
        }

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

        if (ks.IsKeyDown(Keys.E) && previousKeyboardState.IsKeyUp(Keys.E))
        {
            isTakingDamage = true;
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


