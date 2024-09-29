using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class BlueBoomer : ILinkItems
    {
        public int direction = 0;
        int startX = 0;
        int startY = 0;
        List<Rectangle> sourceRects = new List<Rectangle> ();
        String directionStr;
        
        public BlueBoomer(Link link)
        {
            string directionStr = "Down";
            sourceRects.Add(new Rectangle(89, 33, 8, 11));
            sourceRects.Add(new Rectangle(97, 33, 10, 10));
            sourceRects.Add(new Rectangle(106, 35, 10, 7)); //116, 42

            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Down";
                    startX = -15;
                    startY = 25; 
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Up";
                    startX = 85;
                    startY = 60;
                    break;
                case DownFacingLinkState:
                    startX = 25;
                    startY = 75;
                    direction = 2;
                    directionStr = "Right";
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    startX = 55;
                    startY = -15;
                    directionStr = "Left";
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX + startX, link.y + link.offsetY + startY, sourceRects[(link.linkCount % 3)], directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.offsetX -= (link.linkCount <= 10) ? 12 : -12;
            }
            else if (direction == 1)
            {
                link.offsetX += (link.linkCount <= 10) ? 12 : -12;
            }
            else if (direction == 2)
            {
                link.offsetY += (link.linkCount <= 10) ? 12 : -12;
            }
            else if (direction == 3)
            {
                link.offsetY -= (link.linkCount <= 10) ? 12 : -12;
            }

            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX + startX, link.y + link.offsetY + startY, sourceRects[(link.linkCount % 3)], directionStr);
            link.UpdateItem();
        }

        public int getDirection()
        {
            return direction;
        }
    }
}