using Microsoft.Xna.Framework;
using System;

namespace Sprint2Pork
{
    public class Sword : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        public Sword(Link link) {
            string directionStr = "Up";
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Right";
                    rect = new Rectangle(104, 16, 8, 16);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Left";
                    rect = new Rectangle(104, 16, 8, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Up";
                    rect = new Rectangle(104, 16, 8, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    rect = new Rectangle(104, 16, 8, 16);
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