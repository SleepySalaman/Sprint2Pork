using Microsoft.Xna.Framework;
using System;

namespace Sprint2Pork
{
    public class Boomerang : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        public Boomerang(Link link)
        {
            string directionStr = "Down";
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Down";
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Up";
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Right";
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Left";
                    rect = new Rectangle(129, 2, 6, 9);
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