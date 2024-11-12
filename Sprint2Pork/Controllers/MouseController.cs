using Microsoft.Xna.Framework.Input;
using Sprint2Pork;

public class MouseController : IController
{
    private Game1 programGame;
    private MouseState previousMouseState;

    public MouseController(Game1 g)
    {
        programGame = g;
        previousMouseState = Mouse.GetState();
    }

    void IController.Update()
    {
        MouseState currentMouseState = Mouse.GetState();
        bool mouseLeftPressed = currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        bool mouseRightPressed = currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;

        if (mouseLeftPressed)
        {
            programGame.SwitchToNextRoom();
        }
        else if (mouseRightPressed)
        {
            programGame.SwitchToPreviousRoom();
        }

        previousMouseState = currentMouseState;
    }
}