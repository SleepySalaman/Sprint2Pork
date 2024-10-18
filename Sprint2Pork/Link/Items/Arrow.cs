using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class Arrow : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        string directionStr = "Down";
        int startX = 0;
        int startY = 0;
        public Arrow(Link link)
        {
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Right";
                    startX = -30;
                    startY = 40;
                    rect = new Rectangle(27, 30, 7, 18); // 34 48
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Left";
                    startX = 80;
                    startY = 20;
                    rect = new Rectangle(27, 30, 7, 18);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Up";
                    startX = 35;
                    startY = 80;
                    rect = new Rectangle(27, 30, 7, 18);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    startX = 10;
                    startY = -40;
                    rect = new Rectangle(27, 30, 7, 18);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.X + link.OffsetX + startX, link.Y + link.OffsetY + startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.OffsetX -= 12;
            }
            else if (direction == 1)
            {
                link.OffsetX += 12;
            }
            else if (direction == 2)
            {
                link.OffsetY += 12;
            }
            else if (direction == 3)
            {
                link.OffsetY -= 12;
            }

            //Explosion
            if (link.linkCount >= 19)
            {
                rect = new Rectangle(51, 34, 10, 9); // 170 47
                if (link.directionState is RightFacingLinkState)
                {
                    link.OffsetX += 87;
                    link.OffsetY += 17;
                }
                else if (link.directionState is UpFacingLinkState)
                {
                    link.OffsetX += 25;
                    link.OffsetY -= 15;
                }
                else if (link.directionState is LeftFacingLinkState)
                {
                    link.OffsetY += 50;
                }
                else if (link.directionState is DownFacingLinkState)
                {
                    link.OffsetX += 50;
                    link.OffsetY += 100;
                }
                link.linkItemSprite = new MovingNonAnimatedSprite(link.X + link.OffsetX, link.Y + link.OffsetY, rect, directionStr);
            }

            link.UpdateItem();
        }
    }
}