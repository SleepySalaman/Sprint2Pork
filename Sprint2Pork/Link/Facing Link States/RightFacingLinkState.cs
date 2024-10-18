using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
            rects.Add(new Rectangle(34, 0, 16, 15));
            rects.Add(new Rectangle(50, 0, 16, 15));
            link.linkSprite = new MovingAnimatedSprite(link.X, link.Y, rects, false, 8, "Right");
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
