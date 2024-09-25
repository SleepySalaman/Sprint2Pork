using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    internal class Arrow : ILinkItems
    {
        int direction = 0;
        Rectangle rect = new Rectangle();
        public Arrow(Link link) {
            switch (link.directionState)
            {
                
                case LeftFacingLinkState:
                    direction = 0;
                    rect = new Rectangle(0, 184, 9, 16);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    rect = new Rectangle(9, 184, 16, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    rect = new Rectangle(9, 184, 16, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    rect = new Rectangle(0, 184, 9, 16);
                    break;

            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x, link.y, rect); // non-moving?
        }

        public void Update(Link link)
        {
            link.UseArrow();
        }
    }
}
