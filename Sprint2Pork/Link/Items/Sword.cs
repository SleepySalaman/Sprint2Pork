using Microsoft.Xna.Framework;
using System;

namespace Sprint2Pork
{
    public class Sword : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        int startX = 0;
        int startY = 0;
        public Sword(Link link)
        {
            string directionStr = "Up";
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Right";
                    startX = -10;
                    startY = 45;
                    rect = new Rectangle(34, 0, 8, 16);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Left";
                    startX = 70;
                    startY = 15;
                    rect = new Rectangle(34, 0, 8, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Up";
                    startX = 40;
                    startY = 70;
                    rect = new Rectangle(34, 0, 8, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    startY = -20;
                    rect = new Rectangle(34, 0, 8, 16);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX + startX, link.y + link.offsetY + startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.offsetX -= (link.linkCount <= 10) ? 7 : -7;
            }
            else if (direction == 1)
            {
                link.offsetX += (link.linkCount <= 10) ? 7 : -7;
            }
            else if (direction == 2)
            {
                link.offsetY += (link.linkCount <= 10) ? 7 : -7;
            }
            else if (direction == 3)
            {
                link.offsetY -= (link.linkCount <= 10) ? 7 : -7;
            }
            link.UpdateItem();
            link.UpdateItem();
        }
    }
}