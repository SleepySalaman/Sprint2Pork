using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2Pork;
using Sprint2Pork.Blocks;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

public class KeyboardController : IController
{
    private Game1 programGame;
    private Link link;
    private List<Block> listOfBlocks;
    private int currentBlockIndex;

    private double timeSinceLastBlockSwitch;
    private double blockSwitchCooldown = 0.3;

    private double timeSinceLastItemSwitch;
    private double itemSwitchCooldown = 0.3;

    private double timeSinceLastEnemySwitch;
    private double enemySwitchCooldown = 0.3;

    public KeyboardController(Game1 g, Link linkCharacter, List<Block> blocks)
    {
        programGame = g;
        link = linkCharacter;
        listOfBlocks = blocks;
        currentBlockIndex = 0;
    }
    public void UpdateLink(Link newLink)
    {
        link = newLink;
    }
    void IController.Update()
    {
        KeyboardState ks = Keyboard.GetState();

        timeSinceLastBlockSwitch += 1 / 60.0; // Assuming 60 FPS; adjust if necessary
        timeSinceLastItemSwitch += 1 / 60.0;
        timeSinceLastEnemySwitch += 1 / 60.0;

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

        // Rotate Blocks (t and y)
        if (ks.IsKeyDown(Keys.T) && timeSinceLastBlockSwitch >= blockSwitchCooldown)
        {
            currentBlockIndex = (currentBlockIndex + 1) % listOfBlocks.Count;
            programGame.CurrentBlockIndex = currentBlockIndex; // Update current block index in Game1
            programGame.UpdateCurrentBlock(); // Call method to update block logic
            timeSinceLastBlockSwitch = 0; // Reset the timer
        }
        if (ks.IsKeyDown(Keys.Y) && timeSinceLastBlockSwitch >= blockSwitchCooldown)
        {
            currentBlockIndex = (currentBlockIndex - 1 + listOfBlocks.Count) % listOfBlocks.Count;
            programGame.CurrentBlockIndex = currentBlockIndex; // Update current block index in Game1
            programGame.UpdateCurrentBlock(); // Call method to update block logic
            timeSinceLastBlockSwitch = 0; // Reset the timer
        }

        // Rotate Items (u and i)
        if (ks.IsKeyDown(Keys.U) && timeSinceLastItemSwitch >= itemSwitchCooldown)
        {
            programGame.PreviousItem();
            timeSinceLastItemSwitch = 0;
        }
        if (ks.IsKeyDown(Keys.I) && timeSinceLastItemSwitch >= itemSwitchCooldown)
        {
            programGame.NextItem();
            timeSinceLastItemSwitch = 0; // Reset the timer
        }

        if (ks.IsKeyDown(Keys.O) && timeSinceLastEnemySwitch >= enemySwitchCooldown)
        {
            programGame.cycleEnemiesBackwards();
            timeSinceLastEnemySwitch = 0;
        }
        if (ks.IsKeyDown(Keys.P) && timeSinceLastEnemySwitch >= enemySwitchCooldown)
        {
            programGame.cycleEnemies();
            timeSinceLastEnemySwitch = 0;
        }
        if (ks.IsKeyDown(Keys.R))
        {
            programGame.ResetGame();
        }
        if (ks.IsKeyDown(Keys.Q))
        {
            programGame.Exit();
        }
    }
}
