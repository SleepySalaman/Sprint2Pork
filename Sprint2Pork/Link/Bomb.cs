using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class Bomb : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        public Bomb(Link link) {
            switch (link.directionState)
            {
                
                case LeftFacingLinkState:
                    direction = 0;
                    rect = new Rectangle(136, 0, 7, 14); // 144, 14
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    rect = new Rectangle(136, 0, 7, 14);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    rect = new Rectangle(136, 0, 7, 14);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    rect = new Rectangle(136, 0, 7, 14);
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
