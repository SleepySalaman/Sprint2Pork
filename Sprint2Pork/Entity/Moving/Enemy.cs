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

        protected int fireballID = 0;

        protected int relativeX = 0;

        protected List<Rectangle> sourceRects;
        protected Rectangle destinationRect;
        protected Rectangle collisionRect;

        protected Color color = Color.White;
        protected int health;

        protected Rectangle healthSourceRect = new Rectangle(210, 260, 100, 100);

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

        public void Draw(SpriteBatch sb, Texture2D txt, Texture2D livesTxt, Texture2D hitboxTxt, bool showHitbox)
        {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], color);
            DrawHitbox(sb, hitboxTxt, showHitbox);
            DrawLives(sb, livesTxt);
        }

        private void DrawLives(SpriteBatch sb, Texture2D txt)
        {
            for (int i = 0; i < health; i++)
            {
                sb.Draw(txt, new Rectangle(collisionRect.X + (20 * i), collisionRect.Y - 20, 20, 20), healthSourceRect, Color.White);
            }
        }

        public void TakeDamage()
        {
            if (health > 0)
            {
                health--;
            }
        }

        private void DrawHitbox(SpriteBatch sb, Texture2D hitboxTxt, bool showHitbox)
        {
            if (showHitbox)
            {
                sb.Draw(hitboxTxt, new Rectangle(collisionRect.X, collisionRect.Y, collisionRect.Width, 1), Color.White);
                sb.Draw(hitboxTxt, new Rectangle(collisionRect.X + collisionRect.Width - 1, collisionRect.Y, 1, collisionRect.Height), Color.White);
                sb.Draw(hitboxTxt, new Rectangle(collisionRect.X, collisionRect.Y + collisionRect.Height - 1, collisionRect.Width, 1), Color.White);
                sb.Draw(hitboxTxt, new Rectangle(collisionRect.X, collisionRect.Y, 1, collisionRect.Height), Color.White);
            }
        }

        public int getX()
        {
            return relativeX;
        }

        public Rectangle GetRect()
        {
            return collisionRect;
        }

        void IEnemy.UpdateFromCollision(bool collides, Color c)
        {
            if (collides)
            {
                color = c;
                TakeDamage();
            }
            else
            {
                color = Color.White;
            }
        }

        public int getHealth()
        {
            return health;
        }

        public abstract int getTextureIndex();

        public int GetFireballID() {
            return fireballID;
        }

    }
}
