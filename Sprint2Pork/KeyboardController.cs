using Microsoft.Xna.Framework.Input;
using Sprint2Pork;

public class KeyboardController : IController
{
    private Game1 programGame;
    private Link link;

    public KeyboardController(Game1 g, Link linkCharacter)
    {
        programGame = g;
        link = linkCharacter;
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
        if (ks.IsKeyDown(Keys.S))
        {
            link.UseItem(1); // Assuming 1 represents using an arrow or some item
        }

        // Rotate Blocks (t and y)
        if (ks.IsKeyDown(Keys.T))
        {
            // Rotate blocks logic (e.g., rotate left)
        }
        if (ks.IsKeyDown(Keys.Y))
        {
            // Rotate blocks logic (e.g., rotate right)
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
        if (ks.IsKeyDown(Keys.Escape))
        {
            programGame.Exit();
        }
    }
}
