﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Windows.Forms.LinkLabel;

namespace Sprint2Pork
{
    public class Bomb : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        string directionStr = "Down";
        public ISprite sprite;
        int startX = 0;
        int startY = 0;
        bool collided = false;
        Link link;

        public Bomb(ILinkDirectionState state, int X, int Y)
        {
            startX += X;
            startY += Y;
            switch (state)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    startX += -30;
                    startY += 2;
                    rect = new Rectangle(127, 29, 9, 16); //136 45
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    startX += 45;
                    startY += 2;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    startX += 10;
                    startY += 30;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    startX += 10;
                    startY += -48;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
            }
            sprite = new MovingNonAnimatedSprite(startX, startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            this.link = link;
            switch (direction)
            {
                case 1:
                    link.OffsetXSet(-10);
                    break;
                case 2:
                    link.OffsetXSet(10);
                    break;
                case 3:
                    link.OffsetYSet(10);
                    break;
                case 4:
                    link.OffsetYSet(-10);
                    break;
            }

            //Explosion
            if (link.LinkCountGet() >= 58)
            {
                rect = new Rectangle(153, 29, 17, 28);
                if (link.directionState is RightFacingLinkState)
                {
                    link.OffsetXChange(87);
                }
                else if (link.directionState is UpFacingLinkState)
                {
                    link.OffsetYChange(-87);
                    link.OffsetXChange(-25);
                }
                else if (link.directionState is LeftFacingLinkState)
                {
                    link.OffsetXChange(-15);
                }
                sprite = new MovingNonAnimatedSprite(startX, startY, rect, directionStr);
            }
            link.UpdateItem();

        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sprite.Draw(sb, texture);
        }
        public bool Collides(Rectangle rect2)
        {
            if (collided)
            {
                return false;
            }
            Rectangle rect1 = sprite.GetRect();
            if (rect1.X + rect1.Width > rect2.X &&
                rect1.X < rect2.X + rect2.Width &&
            rect1.Y + rect1.Height > rect2.Y &&
                rect1.Y < rect2.Y + rect2.Height && link.LinkCountGet() >= 58)
            {
                collided = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public Rectangle getLocation() => (sprite.GetRect());
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
        public string GetItemName() => "GroundBomb";

    }
}