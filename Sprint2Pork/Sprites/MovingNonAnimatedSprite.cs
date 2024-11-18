using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class MovingNonAnimatedSprite : ISprite
{
    private readonly int currentFrame;
    private readonly int spriteWidth;
    private readonly int spriteHeight;
    private readonly List<Rectangle> sourceRects;
    private Rectangle destinationRect;
    private readonly string direction;

    public MovingNonAnimatedSprite(int x, int y, Rectangle rect, string direction)
    {
        sourceRects = new List<Rectangle> { rect };
        spriteWidth = rect.Width;
        spriteHeight = rect.Height;
        currentFrame = 0;
        this.direction = direction;
        destinationRect = new Rectangle(x, y, rect.Width * 3, rect.Height * 3);
    }

    void ISprite.Update(int x, int y)
    {
        destinationRect.X = x;
        destinationRect.Y = y;
    }

    void ISprite.Draw(SpriteBatch sb, Texture2D txt)
    {
        float rotation = 0f;
        Vector2 origin = new Vector2(0, 0);

        if (direction == "Left")
        {
            rotation = MathHelper.PiOver2; // 90 degrees
        }
        else if (direction == "Right")
        {
            rotation = -MathHelper.PiOver2; // -90 degrees
        }
        else if (direction == "Up")
        {
            rotation = MathHelper.Pi; // 180 degrees
        }

        sb.Draw(txt, destinationRect, sourceRects[currentFrame], Color.White, rotation, origin, SpriteEffects.None, 0);
    }

    Rectangle ISprite.GetRect()
    {
        return destinationRect;
    }
}