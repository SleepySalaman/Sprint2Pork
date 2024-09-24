using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class RightFacingLinkState : ILinkDirectionState
    {
        private Link link;
        private ISprite linkSprite;

        public RightFacingLinkState(Link link)
        {
            this.link = link;
            List<Rectangle> rects = new List<Rectangle>();
            rects.Add(new Rectangle(35, 11, 16, 16));
            rects.Add(new Rectangle(52, 11, 16, 16));
            link.linkSprite = new MovingAnimatedSprite(link.x, link.y, rects, false);
        }

        public void Update()
        {
            //link.LookRight();
        }

        public void LookLeft()
        {
            link.directionState = new LeftFacingLinkState(link);
        }

        public void LookRight()
        {
            // NO-OP
        }

        public void LookUp()
        {
            link.directionState = new UpFacingLinkState(link);
        }

        public void LookDown()
        {
            link.directionState = new DownFacingLinkState(link);
        }
    }
}
