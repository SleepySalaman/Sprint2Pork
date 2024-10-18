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
    private int interval;
    private List<Rectangle> sourceRects;
    private Rectangle destinationRect;
    private int count;
    private string direction;

    public MovingAnimatedSprite(int x, int y, List<Rectangle> rects, bool flipped, int frameInt, string direction)
    {
        sourceRects = rects;
        Rectangle rect = rects[0];
        spriteWidth = rect.Width;
        spriteHeight = rect.Height;
        interval = frameInt;
        count = 0;
        currentFrame = 0;
        totalFrames = sourceRects.Count;
        this.flipped = flipped;
        this.direction = direction;
        destinationRect = new Rectangle(x, y, 100, 0);
    }

    void ISprite.Update(int x, int y)
    {
        CountFrames();
        // Update the width and height of the destination rectangle based on the current frame's source rectangle
        DynamicallyAdjustDestinationRectangleSize(x, y);
    }

    private void CountFrames()
    {
        count++;
        if (count > interval)
        {
            currentFrame++;
            count = 0;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }
    }

    private void DynamicallyAdjustDestinationRectangleSize(int x, int y)
    {
        destinationRect.Width = sourceRects[currentFrame].Width * 3;
        destinationRect.Height = sourceRects[currentFrame].Height * 3;

        // Adjust the position based on the direction
        if (direction == "Left")
        {
            destinationRect.X = x - destinationRect.Width + spriteWidth * 3;
            destinationRect.Y = y;
        }
        else if (direction == "Up")
        {
            destinationRect.X = x;
            destinationRect.Y = y - destinationRect.Height + spriteHeight * 3;
        }
        else
        {
            destinationRect.X = x;
            destinationRect.Y = y;
        }
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

    Rectangle ISprite.GetRect()
    {
        return destinationRect;
    }
}
