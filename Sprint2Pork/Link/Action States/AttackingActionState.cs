using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class AttackingActionState : ILinkActionState
    {
        private Link link;

        public AttackingActionState(Link link)
        {
            this.link = link;
            List<Rectangle> rects = new List<Rectangle>();
            bool flipped = false;

            switch (link.directionState)
            {
                // TODO: Update rects to be correct attack animations for each direction
                case LeftFacingLinkState:
                    // no sword
                    rects.Add(new Rectangle(0, 43, 16, 16));
                    // half extended brown
                    rects.Add(new Rectangle(69, 43, 22, 16));
                    // fully extended brown
                    rects.Add(new Rectangle(45, 43, 23, 16));
                    // fully extended blue
                    rects.Add(new Rectangle(16, 43, 28, 16));

                    // fully extended brown
                    rects.Add(new Rectangle(45, 43, 23, 16));
                    // half extended brown
                    rects.Add(new Rectangle(69, 43, 22, 16));
                    // no sword
                    rects.Add(new Rectangle(0, 43, 16, 16));
                    flipped = true;
                    link.linkSprite = new MovingAnimatedSprite(link.GetX(), link.GetY(), rects, flipped, 5, "Left"); // non-moving?
                    break;
                case RightFacingLinkState:
                    // no sword
                    rects.Add(new Rectangle(0, 43, 16, 16));
                    // half extended brown
                    rects.Add(new Rectangle(69, 43, 22, 16));
                    // fully extended brown
                    rects.Add(new Rectangle(45, 43, 23, 16));
                    // fully extended blue
                    rects.Add(new Rectangle(16, 43, 28, 16));

                    // fully extended brown
                    rects.Add(new Rectangle(45, 43, 23, 16));
                    // half extended brown
                    rects.Add(new Rectangle(69, 43, 22, 16));
                    // no sword
                    rects.Add(new Rectangle(0, 43, 16, 16));
                    link.linkSprite = new MovingAnimatedSprite(link.GetX(), link.GetY(), rects, flipped, 5, "Right"); // non-moving?
                    break;
                case UpFacingLinkState:
                    // no sword
                    rects.Add(new Rectangle(0, 86, 16, 17));
                    // short sword
                    rects.Add(new Rectangle(50, 82, 16, 22));
                    // 3/4 sword blue
                    rects.Add(new Rectangle(34, 75, 16, 29));
                    // full sword blue
                    rects.Add(new Rectangle(17, 75, 16, 29));
                    // 3/4 sword blue
                    rects.Add(new Rectangle(34, 75, 16, 29));
                    // short sword
                    rects.Add(new Rectangle(50, 82, 16, 22));
                    // no sword
                    rects.Add(new Rectangle(0, 86, 16, 17));
                    link.linkSprite = new MovingAnimatedSprite(link.GetX(), link.GetY(), rects, flipped, 5, "Up"); // non-moving?
                    break;
                case DownFacingLinkState:
                    // no sword
                    rects.Add(new Rectangle(0, 16, 16, 16));
                    //short sword
                    rects.Add(new Rectangle(50, 16, 16, 22));
                    // half sword
                    rects.Add(new Rectangle(33, 16, 16, 26));
                    // full sword blue
                    rects.Add(new Rectangle(16, 16, 16, 27));
                    // half sword
                    rects.Add(new Rectangle(33, 16, 16, 26));
                    //short sword
                    rects.Add(new Rectangle(50, 16, 16, 22));
                    // no sword
                    rects.Add(new Rectangle(0, 16, 16, 16));
                    link.linkSprite = new MovingAnimatedSprite(link.GetX(), link.GetY() , rects, flipped, 5, "Down"); // non-moving?
                    break;
            }
        }

        public void BeIdle()
        {
            link.actionState = new IdleActionState(link);
        }

        public void BeMoving()
        {
            link.actionState = new MovingActionState(link);
        }

        public void BeAttacking()
        {
            // NO-OP
        }

        public void TakeDamage()
        {
            // NO-OP
        }

        public void Update()
        {
            link.Attack();
        }
    }
}
