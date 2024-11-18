using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class BlueBoomer : ILinkItems
    {
        public int direction = 0;
        int startX = 0;
        int startY = 0;
        List<Rectangle> sourceRects = new List<Rectangle>();
        public ISprite sprite;
        String directionStr = "Down";

        public BlueBoomer(ILinkDirectionState state, int X, int Y)
        {
            string directionStr = "Down";
            sourceRects.Add(new Rectangle(89, 33, 8, 11));
            sourceRects.Add(new Rectangle(97, 33, 10, 10));
            sourceRects.Add(new Rectangle(106, 35, 10, 7)); //116, 42

            switch (state)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Down";
                    startX = -15;
                    startY = 10;
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Up";
                    startX = 35;
                    startY = 10;
                    break;
                case DownFacingLinkState:
                    startX = 15;
                    startY = 20;
                    direction = 2;
                    directionStr = "Right";
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    startX = 5;
                    startY = -5;
                    directionStr = "Left";
                    break;
            }

            sprite = new MovingNonAnimatedSprite(startX, startY, sourceRects[(0)], directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.OffsetXChange((link.LinkCountGet() <= 10) ? -12 : 12);
            }
            else if (direction == 1)
            {
                link.OffsetXChange((link.LinkCountGet() <= 10) ? 12 : -12);
            }
            else if (direction == 2)
            {
                link.OffsetYChange((link.LinkCountGet() <= 10) ? 12 : -12);
            }
            else if (direction == 3)
            {
                link.OffsetYChange((link.LinkCountGet() <= 10) ? -12 : 12);
            }

            sprite = new MovingNonAnimatedSprite(link.GetX() + link.OffsetXGet() + startX, link.GetY() + link.OffsetYGet() + startY, sourceRects[(link.LinkCountGet() % 3)], directionStr);
            link.UpdateItem();
        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sprite.Draw(sb, texture);
        }
        public bool Collides(Rectangle rect2)
        {
            Rectangle rect1 = sprite.GetRect();
            return (rect1.X + rect1.Width > rect2.X &&
                rect1.X < rect2.X + rect2.Width &&
                rect1.Y + rect1.Height > rect2.Y &&
                rect1.Y < rect2.Y + rect2.Height);
        }
        public Rectangle getLocation() => (sprite.GetRect());
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
        public string GetItemName() => "BlueBoomer";

    }
}