using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint2Pork;
using System;
using System.Xml;

public class KeyboardController : IController
{

    private Game1 programGame;

    public KeyboardController(Game1 g)
    {
        programGame = g;
    }

    void IController.Update()
    {
        KeyboardState ks = Keyboard.GetState();
        if (ks.IsKeyDown(Keys.D1) || ks.IsKeyDown(Keys.NumPad1))
        {
            programGame.setMode(Game1.PlayerSpriteList.NonMovingNonAnimatedPlayer);
        }
        if (ks.IsKeyDown(Keys.D2) || ks.IsKeyDown(Keys.NumPad2))
        {
            programGame.setMode(Game1.PlayerSpriteList.NonMovingAnimatedPlayer);
        }
        if (ks.IsKeyDown(Keys.D3) || ks.IsKeyDown(Keys.NumPad3))
        {
            programGame.setMode(Game1.PlayerSpriteList.MovingNonAnimatedPlayer);
        }
        if (ks.IsKeyDown(Keys.D4) || ks.IsKeyDown(Keys.NumPad4))
        {
            programGame.setMode(Game1.PlayerSpriteList.MovingAnimatedPlayer);
        }
    }
}
