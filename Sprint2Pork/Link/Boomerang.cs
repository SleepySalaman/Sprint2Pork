using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class Boomerang : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        public Boomerang(Link link) {
            switch (link.directionState)
            {
                
                case LeftFacingLinkState:
                    direction = 0;
                    rect = new Rectangle(129, 2, 6, 9); //134, 11
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    rect = new Rectangle(129, 2, 6, 9);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    rect = new Rectangle(129, 2, 6, 9);
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
