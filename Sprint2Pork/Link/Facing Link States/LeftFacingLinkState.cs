using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
            rects.Add(new Rectangle(34, 0, 16, 15));
            rects.Add(new Rectangle(50, 0, 16, 15));
            link.linkSprite = new MovingAnimatedSprite(link.GetX(), link.Y, rects, true, 8, "Left");
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
