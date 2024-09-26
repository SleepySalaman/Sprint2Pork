using Microsoft.Xna.Framework;
using System;

namespace Sprint2Pork
{
    public class Bomb : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        public Bomb(Link link) {
            string directionStr = "Down"; // Default direction
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Down";
                    rect = new Rectangle(136, 0, 8, 14);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Down";
                    rect = new Rectangle(136, 0, 8, 14);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Down";
                    rect = new Rectangle(136, 0, 8, 14);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    rect = new Rectangle(136, 0, 8, 14);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX, link.y + link.offsetY, rect, directionStr);
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