using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class Fire : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        int startX = 0;
        int startY = 0;
        public Fire(Link link)
        {
            string directionStr = "Down";
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    startX = -50;
                    startY = 0;
                    rect = new Rectangle(188, 30, 19, 18); // 207 48
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    startX = 30;
                    startY = -0;
                    rect = new Rectangle(188, 30, 19, 18);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    startX = 0;
                    startY = 20;
                    rect = new Rectangle(188, 30, 19, 18);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    startX = -10;
                    startY = -40;
                    rect = new Rectangle(188, 30, 19, 18);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.GetX() + link.OffsetX + startX, link.GetY() + link.OffsetY + startY, rect, directionStr);
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
            link.UpdateItem();
        }
    }
}