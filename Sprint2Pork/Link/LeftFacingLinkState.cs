using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class LeftFacingLinkState : ILinkDirectionState
    {
        private Link link;
        //private ISprite linkSprite;

        public LeftFacingLinkState(Link link)
        {
            this.link = link;
            List<Rectangle> rects = new List<Rectangle>();
            rects.Add(new Rectangle(33, 0, 16, 16));
            rects.Add(new Rectangle(50, 0, 16, 16));
            link.linkSprite = new MovingAnimatedSprite(link.x, link.y, rects, true, 15);
        }

        public void Update()
        {
            //link.LookLeft();
        }

        public void LookLeft()
        {
            // NO-OP
        }

        public void LookRight()
        {
            link.directionState = new RightFacingLinkState(link);
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
