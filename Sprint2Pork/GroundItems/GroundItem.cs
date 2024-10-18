using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.GroundItems;
using System.Collections.Generic;

namespace Sprint2Pork.Items
{
    public abstract class GroundItem : IGroundItem
    {
        protected List<Rectangle> sourceRects;
        public Rectangle destinationRect;
        protected int currentFrame;
        protected int totalFrames;
        protected int count;
        protected Color c = Color.White;

        public GroundItem(int x, int y, List<Rectangle> frames)
        {
            sourceRects = frames;
            currentFrame = 0;
            totalFrames = sourceRects.Count;
            destinationRect = new Rectangle(x, y, 32, 32);
            count = 0;
        }

        public void Update(int x, int y)
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
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, destinationRect, sourceRects[currentFrame], c);
        }

        public abstract void PerformAction();

        public Rectangle GetRect()
        {
            return destinationRect;
        }

        public void udpateFromCollision(bool collides)
        {
            if (collides)
            {
                c = Color.Red;
            }
            else
            {
                c = Color.White;
            }
        }
    }
}