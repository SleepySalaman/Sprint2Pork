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
                    link.OffsetXSet(-30);
                    link.OffsetYSet(2);
                    rect = new Rectangle(127, 29, 9, 16); //136 45
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    link.OffsetXSet(45);
                    link.OffsetYSet(2);
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    link.OffsetXSet(10);
                    link.OffsetYSet(30);
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    link.OffsetXSet(10);
                    link.OffsetYSet(-48);
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.GetX() + link.OffsetXGet(), link.GetY() + link.OffsetYGet(), rect, directionStr);
        }

        public void Update(Link link)
        {
            switch (direction)
            {
                case 1:
                    link.OffsetXSet(-10);
                    break;
                case 2:
                    link.OffsetXSet(10);
                    break;
                case 3:
                    link.OffsetYSet(10);
                    break;
                case 4:
                    link.OffsetYSet(-10);
                    break;
            }

            //Explosion
            if (link.linkCount >= 18)
            {
                rect = new Rectangle(153, 29, 17, 28);
                if (link.directionState is RightFacingLinkState)
                {
                    link.OffsetXChange(87);
                }
                else if (link.directionState is UpFacingLinkState)
                {
                    link.OffsetYChange(-87);
                    link.OffsetXChange(-25);
                }
                else if (link.directionState is LeftFacingLinkState)
                {
                    link.OffsetXChange(-15);
                }
                link.linkItemSprite = new MovingNonAnimatedSprite(link.GetX() + link.OffsetXGet(), link.GetY() + link.OffsetYGet(), rect, directionStr);
            }
            link.UpdateItem();
        }

    }
}