﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class Boomerang : ILinkItems
    {
        public int direction = 0;
        int startX = 0;
        int startY = 0;
        private ISprite sprite;

        string directionStr = "Down";
        List<Rectangle> sourceRects = new List<Rectangle>();
        public Boomerang(ILinkDirectionState state, int X, int Y)
        {
            sourceRects.Add(new Rectangle(62, 32, 8, 11));
            sourceRects.Add(new Rectangle(70, 32, 10, 11));
            sourceRects.Add(new Rectangle(78, 36, 11, 6));

            startX += X;
            startY += Y;
            switch (state)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Down";
                    startX += -15;
                    startY += 10;
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Up";
                    startX += 85;
                    startY += 50;
                    break;
                case DownFacingLinkState:
                    startX += 15;
                    startY += 65;
                    direction = 2;
                    directionStr = "Right";
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    startX += 45;
                    startY += -15;
                    directionStr = "Left";
                    break;
            }
            sprite = new MovingNonAnimatedSprite(startX, startY, sourceRects[0], directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.OffsetXChange((link.LinkCountGet() <= 10) ? -7 : 7);
            }
            else if (direction == 1)
            {
                link.OffsetXChange((link.LinkCountGet() <= 10) ? 7 : -7);
            }
            else if (direction == 2)
            {
                link.OffsetYChange((link.LinkCountGet() <= 10) ? 7 : -7);
            }
            else if (direction == 3)
            {
                link.OffsetYChange((link.LinkCountGet() <= 10) ? -7 : 7);
            }

            sprite = new MovingNonAnimatedSprite(startX + link.OffsetXGet(), startY + link.OffsetYGet(), sourceRects[(link.LinkCountGet() % 3)], directionStr);
            link.UpdateItem();
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sprite.Draw(sb, texture);
        }

        public (int X, int Y) getLocation(Link link) => (startX + link.OffsetXGet(), startY + link.OffsetYGet());
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
    }
}