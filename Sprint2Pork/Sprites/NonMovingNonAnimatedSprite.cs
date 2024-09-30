using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class NonMovingNonAnimatedSprite : ISprite
{
    private int totalFrames;
    private int currentFrame;
    private bool flipped;
    private List<Rectangle> sourceRects;
    private Rectangle destinationRect;

    public NonMovingNonAnimatedSprite(int x, int y, Rectangle rect, bool flipped)
    {
        sourceRects = new List<Rectangle> { rect };
        this.flipped = flipped;
        currentFrame = 0;
        totalFrames = sourceRects.Count;

        // Apply the same scaling factor as MovingAnimatedSprite
        destinationRect = new Rectangle(x, y, rect.Width * 3, rect.Height * 3);
    }

    void ISprite.Update(int x, int y)
    {
        currentFrame++;
        if (currentFrame == totalFrames)
        {
            currentFrame = 0;
        }
        destinationRect.X = x;
        destinationRect.Y = y;
    }

    void ISprite.Draw(SpriteBatch sb, Texture2D txt)
    {
        if (!flipped)
        {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], Color.White);
        }
        else
        {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
        }
    }
}
