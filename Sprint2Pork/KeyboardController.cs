using Microsoft.Xna.Framework.Input;
using Sprint2Pork.Blocks;
using Sprint2Pork;
using System.Collections.Generic;

public class KeyboardController : IController
{
    private Game1 programGame;
    private Link link;
    private List<Block> listOfBlocks;
    private int currentBlockIndex;

    private KeyboardState previousKeyboardState;

    // Damage effect fields
    private int damageEffectCounter;
    private bool isTakingDamage;
    private const int flashRate = 5; 

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
            isTakingDamage = link.BeDamaged();
        }
        else
        {
            HandleMovement(ks);
            HandleItemUse(ks, linkItem);
            HandleGameControls(ks);
        }

        previousKeyboardState = ks;
    }

    //private void HandleDamageEffect()
    //{
    //    if (damageEffectCounter % flashRate == 0)
    //    {
    //        if ((damageEffectCounter / flashRate) % 2 == 0)
    //        {
    //            link.TakeDamage();
    //        }
    //        else
    //        {
    //            link.BeIdle();
    //        }
    //    }

    //    damageEffectCounter++;
    //    if (damageEffectCounter >= flashRate * 10)
    //    {
    //        isTakingDamage = false;
    //        damageEffectCounter = 0;
    //    }
    //}

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
        if (IsKeyPressed(ks, Keys.D1) || linkItem is Arrow)
        {
            link.BeAttacking();
            link.UseItem(1);
        }
        else if (IsKeyPressed(ks, Keys.D2) || linkItem is Boomerang)
        {
            link.BeAttacking();
            link.UseItem(2);
        }
        else if (IsKeyPressed(ks, Keys.D3) || linkItem is Bomb)
        {
            link.BeAttacking();
            link.UseItem(3);
        }
        else if (IsKeyPressed(ks, Keys.D4) || linkItem is WoodArrow)
        {
            link.BeAttacking();
            link.UseItem(4);
        }
        else if (IsKeyPressed(ks, Keys.D5) || linkItem is BlueBoomer)
        {
            link.BeAttacking();
            link.UseItem(5);
        }
        else if (IsKeyPressed(ks, Keys.D6) || linkItem is Fire)
        {
            link.BeAttacking();
            link.UseItem(6);
        }
        else if (IsKeyPressed(ks, Keys.Z) || IsKeyPressed(ks, Keys.N) || linkItem is Sword)
        {
            link.BeAttacking();
            link.UseItem(0);
        }

        if (ks.IsKeyDown(Keys.Z) || ks.IsKeyDown(Keys.N))
        {
            link.BeAttacking();
        }

        if (IsKeyPressed(ks, Keys.E))
        {
            isTakingDamage = true;
        }
    }

    private void HandleGameControls(KeyboardState ks)
    {
        if (IsKeyPressed(ks, Keys.T))
        {
            currentBlockIndex = (currentBlockIndex + 1) % listOfBlocks.Count;
            UpdateCurrentBlock();
        }
        else if (IsKeyPressed(ks, Keys.Y))
        {
            currentBlockIndex = (currentBlockIndex - 1 + listOfBlocks.Count) % listOfBlocks.Count;
            UpdateCurrentBlock();
        }

        if (IsKeyPressed(ks, Keys.U))
        {
            programGame.PreviousItem();
        }
        if (IsKeyPressed(ks, Keys.I))
        {
            programGame.NextItem();
        }

        if (IsKeyPressed(ks, Keys.O))
        {
            programGame.cycleEnemiesBackwards();
        }
        if (IsKeyPressed(ks, Keys.P))
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
        if (IsKeyPressed(ks, Keys.F))
        {
            programGame.IsFullscreen = !programGame.IsFullscreen;
            programGame.graphics.IsFullScreen = programGame.IsFullscreen;
            programGame.graphics.ApplyChanges();
        }
        if (IsKeyPressed(ks, Keys.G))
        {
            programGame.SwitchToNextRoom();
        }
        else if (IsKeyPressed(ks, Keys.H))
        {
            programGame.SwitchToPreviousRoom();
        }
    }

    private void UpdateCurrentBlock()
    {
        programGame.CurrentBlockIndex = currentBlockIndex;
        programGame.UpdateCurrentBlock();
    }

    private bool IsKeyPressed(KeyboardState ks, Keys key)
    {
        return ks.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
    }
}
