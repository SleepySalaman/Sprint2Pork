using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public abstract class Enemy : IEnemy
    {

        protected int totalFrames;
        protected int currentFrame = 0;

        protected int count = 0;
        protected int maxCount = 6;

        protected int rectW = 100;
        protected int rectH = 100;

        protected int previousX = 0;
        protected int previousY = 0;

        protected int relativeX = 0;

        protected List<Rectangle> sourceRects;
        protected Rectangle destinationRect;

        protected Color color = Color.White;

        protected Rectangle roomBoundingBox = new Rectangle(120, 165, 560, 275);
        public void Update()
        {
            count++;
            if (count > maxCount)
            {
                currentFrame++;
                count = 0;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }

        public abstract void Move(List<Block> blocks);

        public void Draw(SpriteBatch sb, Texture2D txt)
        {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], color);
        }

        public int getX()
        {
            return relativeX;
        }

        public Rectangle getRect()
        {
            return destinationRect;
        }

        void IEnemy.updateFromCollision(bool collides, Color c)
        {
            if (collides)
            {
                color = c;
            }
            else
            {
                color = Color.White;
            }
        }

        public abstract int getTextureIndex();

    }
}
