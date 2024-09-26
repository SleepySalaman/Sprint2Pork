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
    // Add a cooldown timer
    private double timeSinceLastBlockSwitch;
    private double blockSwitchCooldown = 0.1; // Adjust as needed

    public KeyboardController(Game1 g, Link linkCharacter, List<Block> blocks)
    {
        programGame = g;
        link = linkCharacter;
        listOfBlocks = blocks;
        currentBlockIndex = 0;
    }

    void IController.Update()
    {
        KeyboardState ks = Keyboard.GetState();

        // Update the cooldown timer by manually incrementing it
        timeSinceLastBlockSwitch += 1 / 60.0; // Assuming 60 FPS; adjust if necessary

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
        if (ks.IsKeyDown(Keys.U))
        {
            // Rotate items logic (e.g., rotate left)
        }
        if (ks.IsKeyDown(Keys.I))
        {
            // Rotate items logic (e.g., rotate right)
        }

        // Rotate Enemies (o and p)
        if (ks.IsKeyDown(Keys.O))
        {
            // Rotate enemies logic (e.g., rotate left)
        }
        if (ks.IsKeyDown(Keys.P))
        {
            // Rotate enemies logic (e.g., rotate right)
        }

        // Quit Game
        if (ks.IsKeyDown(Keys.Q))
        {
            programGame.Exit();
        }
    }
}
