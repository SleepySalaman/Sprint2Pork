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
    }
}
