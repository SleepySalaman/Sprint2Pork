using Microsoft.Xna.Framework.Input;
using Sprint2Pork;
using Sprint2Pork.Blocks;
using Sprint2Pork.Managers;
using System.Collections.Generic;

public class KeyboardController : IController
{
    private Game1 programGame;
    private Link link;
    private List<Block> listOfBlocks;
    private KeyboardState previousKeyboardState;

    // Damage effect fields
    private bool isTakingDamage;
    private const int flashRate = 5;

    public KeyboardController(Game1 g, Link linkCharacter, List<Block> blocks)
    {
        programGame = g;
        link = linkCharacter;
        listOfBlocks = blocks;
        previousKeyboardState = Keyboard.GetState();

        // Initialize damage effect fields
        isTakingDamage = false;
    }

    public void UpdateLink(Link newLink)
    {
        link = newLink;
    }

    void IController.Update()
    {
        KeyboardState ks = Keyboard.GetState();

        // Handle start screen input
        if (programGame.gameState == Game1State.StartScreen)
        {
            HandleStartScreen(ks);
        }
        else
        {
            ILinkItems linkItem = link.linkItem;

            if (isTakingDamage)
            {
                isTakingDamage = link.BeDamaged();
            }
            else
            {
                HandleMovement(ks);
                HandleItemUse(ks, linkItem);
                HandleGameControls(ks);
            }
        }

        previousKeyboardState = ks;
    }

    private void HandleStartScreen(KeyboardState ks)
    {
        // Press Enter or Space to start the game
        if (IsKeyPressed(ks, Keys.Enter) || IsKeyPressed(ks, Keys.Space))
        {
            programGame.StartGame();
        }

        // Allow quitting from start screen
        if (IsKeyPressed(ks, Keys.Q))
        {
            programGame.Exit();
        }
    }

    private void HandleMovement(KeyboardState ks)
    {
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
    }

    private void HandleItemUse(KeyboardState ks, ILinkItems linkItem)
    {
        if (IsKeyPressed(ks, Keys.D1))
        {
            link.BeAttacking();
            link.UseItem(1);
        }
        else if (IsKeyPressed(ks, Keys.D2))
        {
            link.BeAttacking();
            link.UseItem(2);
        }
        else if (IsKeyPressed(ks, Keys.D3))
        {
            link.BeAttacking();
            link.UseItem(3);
        }
        else if (IsKeyPressed(ks, Keys.D4))
        {
            link.BeAttacking();
            link.UseItem(4);
        }
        else if (IsKeyPressed(ks, Keys.D5))
        {
            link.BeAttacking();
            link.UseItem(5);
        }
        else if (IsKeyPressed(ks, Keys.D6))
        {
            link.BeAttacking();
            link.UseItem(6);
        }
        else if (IsKeyPressed(ks, Keys.Z) || IsKeyPressed(ks, Keys.N) || linkItem is Sword)
        {
            link.BeAttacking();
            link.PlaySound("sfxSwordZap");
            link.UseItem(0);
        }

        // Uses sword (or Item A)
        if (ks.IsKeyDown(Keys.Z) || ks.IsKeyDown(Keys.N))
        {
            link.BeAttacking();
        }

        // Uses item B
        if (IsKeyPressed(ks, Keys.X))
        {
            link.UseItemB();
        }

        // Cycle to the next item for slot B
        if (IsKeyPressed(ks, Keys.J))
        {
            link.NextItem();
        }

        // Cycle to the previous item for slot B
        if (IsKeyPressed(ks, Keys.K))
        {
            link.PreviousItem();
        }
    }

    private void HandleGameControls(KeyboardState ks)
    {
        if (IsKeyPressed(ks, Keys.M))
        {
            programGame.ToggleBackgroundMusic();
        }
        if (IsKeyPressed(ks, Keys.I))
        {
            programGame.TogglePause();
        }

        if (IsKeyPressed(ks, Keys.P))
        {
            programGame.TogglePause();
        }
        if (IsKeyPressed(ks, Keys.O))
        {
            programGame.GameOver();
        }

        if (ks.IsKeyDown(Keys.R))
        {
            programGame.ResetGame();
        }
        if (ks.IsKeyDown(Keys.Q))
        {
            programGame.Exit();
        }
        if (IsKeyPressed(ks, Keys.F))
        {
            programGame.IsFullscreen = !programGame.IsFullscreen;
            programGame.graphics.IsFullScreen = programGame.IsFullscreen;
            programGame.graphics.ApplyChanges();
        }
        if (IsKeyPressed(ks, Keys.F12))
        {
            programGame.GetDevRoom();
        }
        if (IsKeyPressed(ks, Keys.H))
        {
            programGame.showHitboxes = !programGame.showHitboxes;
        }
    }

    private bool IsKeyPressed(KeyboardState ks, Keys key)
    {
        return ks.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
    }
}