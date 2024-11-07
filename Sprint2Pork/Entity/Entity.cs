using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Sprint2Pork.Entity
{
    public abstract class Entity : IEntity
    {

        protected int totalFrames;
        protected int currentFrame = 0;

        protected int count = 0;
        protected int maxCount = 30;

        protected int rectW = 100;
        protected int rectH = 100;

        protected List<Rectangle> sourceRects;
        protected Rectangle destinationRect;

        protected Color c = Color.White;

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

        public void Draw(SpriteBatch sb, Texture2D txt, Texture2D hitboxTxt, bool showHitbox)
        {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], c);
            DrawHitbox(sb, hitboxTxt, showHitbox);
        }

        private void DrawHitbox(SpriteBatch sb, Texture2D hitboxTxt, bool showHitbox) {
            if (showHitbox) {
                sb.Draw(hitboxTxt, new Rectangle(destinationRect.X, destinationRect.Y, destinationRect.Width, 1), Color.White);
                sb.Draw(hitboxTxt, new Rectangle(destinationRect.X + destinationRect.Width - 1, destinationRect.Y, 1, destinationRect.Height), Color.White);
                sb.Draw(hitboxTxt, new Rectangle(destinationRect.X, destinationRect.Y + destinationRect.Height - 1, destinationRect.Width, 1), Color.White);
                sb.Draw(hitboxTxt, new Rectangle(destinationRect.X, destinationRect.Y, 1, destinationRect.Height), Color.White);
            }
        }

        public Rectangle getHitbox()
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

        public Rectangle getRect()
        {
            return destinationRect;
        }
    }
}
