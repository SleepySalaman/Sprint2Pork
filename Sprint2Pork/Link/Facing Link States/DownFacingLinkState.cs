using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class DownFacingLinkState : ILinkDirectionState
    {
        private Link link;

        public DownFacingLinkState(Link link)
        {
            this.link = link;
            List<Rectangle> rects = new List<Rectangle>();
            rects.Add(new Rectangle(0, 0, 16, 15));
            rects.Add(new Rectangle(16, 0, 16, 15));
            link.linkSprite = new MovingAnimatedSprite(link.X, link.Y, rects, false, 8, "Down");
        }

        public void Update()
        {
            //link.LookDown();
        }

        public void LookLeft()
        {
            link.directionState = new LeftFacingLinkState(link);
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
            // NO-OP
        }
    }
}
