using Microsoft.Xna.Framework;
using System;

namespace Sprint2Pork
{
    public class Bomb : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        public Bomb(Link link)
        {
            string directionStr = "Down";
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    link.offsetX = -50;
                    link.offsetY = 15;
                    rect = new Rectangle(136, 0, 8, 14);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    link.offsetX = 87;
                    link.offsetY = 15;
                    rect = new Rectangle(136, 0, 8, 14);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    link.offsetX = 25;
                    link.offsetY = 75;
                    rect = new Rectangle(136, 0, 8, 14);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    link.offsetX = 25;
                    link.offsetY = -75;
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