using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class Sword : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        public Sword(Link link)
        {
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    //link.offsetX = -16;
                    rect = new Rectangle(104, 16, 8, 16);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    //link.offsetX = 16;
                    rect = new Rectangle(104, 16, 8, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    //link.offsetY = 16;
                    rect = new Rectangle(104, 16, 8, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    //link.offsetY = -16;
                    rect = new Rectangle(104, 16, 8, 16);
                    break;

            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX, link.y + link.offsetY, rect); // non-moving?
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
