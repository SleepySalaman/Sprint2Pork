using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class Boomerang : ILinkItems
    {
        public int direction = 0;
        int startX = 0;
        int startY = 0;

        string directionStr = "Down";
        List<Rectangle> sourceRects = new List<Rectangle>();
        public Boomerang(Link link)
        {
            sourceRects.Add(new Rectangle(62, 32, 8, 11));
            sourceRects.Add(new Rectangle(70, 32, 10, 11));
            sourceRects.Add(new Rectangle(78, 36, 11, 6));

            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Down";
                    startX = -15;
                    startY = 10;
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Up";
                    startX = 85;
                    startY = 50;
                    break;
                case DownFacingLinkState:
                    startX = 15;
                    startY = 65;
                    direction = 2;
                    directionStr = "Right";
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    startX = 45;
                    startY = -15;
                    directionStr = "Left";
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.GetX() + link.OffsetXGet() + startX, link.GetY() + link.OffsetYGet() + startY, sourceRects[0], directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.OffsetXChange((link.LinkCountGet() <= 10) ? -7 : 7);
            }
            else if (direction == 1)
            {
                link.OffsetXChange((link.LinkCountGet() <= 10) ? 7 : -7);
            }
            else if (direction == 2)
            {
                link.OffsetYChange((link.LinkCountGet() <= 10) ? 7 : -7);
            }
            else if (direction == 3)
            {
                link.OffsetYChange((link.LinkCountGet() <= 10) ? -7 : 7);
            }

            link.linkItemSprite = new MovingNonAnimatedSprite(link.GetX() + link.OffsetXGet() + startX, link.GetY() + link.OffsetYGet() + startY, sourceRects[(link.LinkCountGet() % 3)], directionStr);
            link.UpdateItem();
        }
    }
}