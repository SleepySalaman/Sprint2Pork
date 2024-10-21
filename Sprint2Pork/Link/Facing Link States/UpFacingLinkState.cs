using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class UpFacingLinkState : ILinkDirectionState
    {
        private Link link;
        private ISprite linkSprite;

        public UpFacingLinkState(Link link)
        {
            this.link = link;
            List<Rectangle> rects = new List<Rectangle>();
            rects.Add(new Rectangle(100, 0, 16, 16));
            rects.Add(new Rectangle(116, 0, 16, 16));
            link.linkSprite = new MovingAnimatedSprite(link.GetX(), link.GetY(), rects, false, 8, "Up");
        }

        public void Update()
        {
            //link.LookUp();
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
            // NO-OP
        }

        public void LookDown()
        {
            link.directionState = new DownFacingLinkState(link);
        }
    }
}
