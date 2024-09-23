using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2Pork;
using System;

public class MouseController : IController
{

    private Game1 programGame;

    public MouseController(Game1 g)
    {
        programGame = g;
    }

    void IController.Update()
    {
        MouseState ms = Mouse.GetState();
        bool mouseDown = ms.LeftButton == ButtonState.Pressed;
        double width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2.2;
        double height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2.2;

        if (mouseDown && ms.X < width / 2 && ms.Y < height / 2)
        {
            programGame.setMode(Game1.PlayerSpriteList.NonMovingNonAnimatedPlayer);
        }
        else if (mouseDown && ms.X < width / 2 && ms.Y >= height / 2)
        {
            programGame.setMode(Game1.PlayerSpriteList.MovingNonAnimatedPlayer);
        }
        else if (mouseDown && ms.X >= width / 2 && ms.Y < height / 2)
        {
            programGame.setMode(Game1.PlayerSpriteList.NonMovingAnimatedPlayer);
        }
        else if (mouseDown)
        {
            programGame.setMode(Game1.PlayerSpriteList.MovingAnimatedPlayer);
        }
    }

    public void Test()
    {

    }
}
