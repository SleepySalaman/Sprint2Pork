using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2Pork;

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
    }

    public void Test()
    {

    }
}
