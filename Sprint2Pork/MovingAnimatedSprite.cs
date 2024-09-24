using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class MovingAnimatedSprite : ISprite
{

    private int totalFrames;
    private int currentFrame;

    private bool flipped;

    private int spriteWidth;
    private int spriteHeight;

    private List<Rectangle> sourceRects;
    private Rectangle destinationRect;

    private int count;

    public MovingAnimatedSprite(int x, int y, List<Rectangle> rects, bool flipped)
    {
        sourceRects = rects;
        Rectangle rect = rects[0];
        spriteWidth = rect.Width;
        spriteHeight = rect.Height;

        count = 0;
        currentFrame = 0;
        totalFrames = sourceRects.Count;

        this.flipped = flipped;

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