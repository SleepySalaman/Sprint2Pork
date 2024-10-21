using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class Bomb : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        string directionStr = "Down";
        public Bomb(Link link)
        {
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    link.OffsetX = -30;
                    link.OffsetY = 2;
                    rect = new Rectangle(127, 29, 9, 16); //136 45
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    link.OffsetX = 45;
                    link.OffsetY = 2;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    link.OffsetX = 10;
                    link.OffsetY = 30;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    link.OffsetX = 10;
                    link.OffsetY = -48;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.GetX() + link.OffsetX, link.Y + link.OffsetY, rect, directionStr);
        }

        public void Update(Link link)
        {
            switch (direction)
            {
                case 1:
                    link.OffsetX = -10;
                    break;
                case 2:
                    link.OffsetX = 10;
                    break;
                case 3:
                    link.OffsetY = 10;
                    break;
                case 4:
                    link.OffsetY = -10;
                    break;
            }

            //Explosion
            if (link.linkCount >= 18)
            {
                rect = new Rectangle(153, 29, 17, 28);
                if (link.directionState is RightFacingLinkState)
                {
                    link.OffsetX += 87;
                }
                else if (link.directionState is UpFacingLinkState)
                {
                    link.OffsetY -= 87;
                    link.OffsetX -= 25;
                }
                else if (link.directionState is LeftFacingLinkState)
                {
                    link.OffsetX -= 15;
                }
                link.linkItemSprite = new MovingNonAnimatedSprite(link.GetX() + link.OffsetX, link.Y + link.OffsetY, rect, directionStr);
            }
            link.UpdateItem();
        }

    }
}