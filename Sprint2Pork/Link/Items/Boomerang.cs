using Microsoft.Xna.Framework;
using System;

namespace Sprint2Pork
{
    public class Boomerang : ILinkItems
    {
        public int direction = 0;
        int startX = 0;
        int startY = 0;
        Rectangle rect = new Rectangle();
        public Boomerang(Link link)
        {
            string directionStr = "Down";
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Down";
                    startX = -15;
                    startY = 25;
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Up";
                    startX = 85;
                    startY = 60;
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
                case DownFacingLinkState:
                    startX = 25;
                    startY = 75;
                    direction = 2;
                    directionStr = "Right";
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    startX = 55;
                    startY = -15;
                    directionStr = "Left";
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX + startX, link.y + link.offsetY + startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            link.UpdateItem();
        }

        public int getDirection()
        {
            return direction;
        }
    }
}