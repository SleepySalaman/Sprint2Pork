using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
                    // Extend sword
                    rects.Add(new Rectangle(0, 48, 16, 15));
                    rects.Add(new Rectangle(16, 48, 16, 15));
                    rects.Add(new Rectangle(42, 48, 16, 15));
                    rects.Add(new Rectangle(60, 48, 16, 15));
                    // Retract sword
                    rects.Add(new Rectangle(42, 48, 16, 15));
                    rects.Add(new Rectangle(16, 48, 16, 15));
                    rects.Add(new Rectangle(0, 48, 16, 15));
                    flipped = true;
                    break;
                case RightFacingLinkState:
                    // Extend sword
                    rects.Add(new Rectangle(0, 48, 16, 15));
                    rects.Add(new Rectangle(16, 48, 16, 15));
                    rects.Add(new Rectangle(42, 48, 16, 15));
                    rects.Add(new Rectangle(60, 48, 16, 15));
                    // Retract sword
                    rects.Add(new Rectangle(42, 48, 16, 15));
                    rects.Add(new Rectangle(16, 48, 16, 15));
                    rects.Add(new Rectangle(0, 48, 16, 15));
                    break;
                case UpFacingLinkState:
                    rects.Add(new Rectangle(0, 0, 16, 15));
                    rects.Add(new Rectangle(50, 0, 16, 15));
                    break;
                case DownFacingLinkState:
                    rects.Add(new Rectangle(0, 0, 16, 15));
                    rects.Add(new Rectangle(50, 0, 16, 15));
                    break;
            }
            link.linkSprite = new MovingAnimatedSprite(link.x, link.y, rects, flipped, 15); // non-moving?
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

        public void Update()
        {
            link.Attack();
        }
    }
}
