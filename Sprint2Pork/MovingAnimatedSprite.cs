using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class MovingAnimatedSprite : ISprite
{

    private int totalFrames;
    private int currentFrame;

    private List<Rectangle> sourceRects;
    private Rectangle destinationRect;

    private int count;

    public MovingAnimatedSprite(int x, int y)
    {
        sourceRects = new List<Rectangle> {
            new Rectangle(84, 0, 16, 16)
        };

        count = 0;
        currentFrame = 0;
        totalFrames = sourceRects.Count;

        destinationRect = new Rectangle(x, y, 100, 100);
    }

    void ISprite.Update(int x, int y)
    {
        count++;
        if (count > 30)
        {
            currentFrame++;
            count = 0;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }
        destinationRect.X = x;
        destinationRect.Y = y;
    }

    void ISprite.Draw(SpriteBatch sb, Texture2D txt)
    {
        sb.Draw(txt, destinationRect, sourceRects[currentFrame], Color.White);
    }
}