using Microsoft.Xna.Framework;

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
            link.linkItemSprite = new MovingNonAnimatedSprite(link.X + link.OffsetX + startX, link.Y + link.OffsetY + startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.OffsetX -= (link.linkCount <= 10) ? 7 : -7;
            }
            else if (direction == 1)
            {
                link.OffsetX += (link.linkCount <= 10) ? 7 : -7;
            }
            else if (direction == 2)
            {
                link.OffsetY += (link.linkCount <= 10) ? 7 : -7;
            }
            else if (direction == 3)
            {
                link.OffsetY -= (link.linkCount <= 10) ? 7 : -7;
            }
            link.UpdateItem();
            link.UpdateItem();
        }
    }
}