using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class DownFacingLinkState : ILinkDirectionState
    {
        private Link link;
        private ISprite linkSprite;

        public DownFacingLinkState(Link link)
        {
            this.link = link;
            //Rectangle sprite = new Rectangle(0, 0, 15, 15);
            link.linkSprite = new NonMovingNonAnimatedSprite(link.x, link.y, new Rectangle(0, 0, 16, 16));
        }

        public void LookLeft()
        {
            //link.directionState = new LeftFacingLinkState(link);
        }

        public void LookRight()
        {
            //link.directionState = new RightFacingLinkState(link);
        }

        public void LookUp()
        {
            //link.directionState = new UpFacingLinkState(link);
        }

        public void LookDown()
        {
            // NO-OP
        }
    }
}
