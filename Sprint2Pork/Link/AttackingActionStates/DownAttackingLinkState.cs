using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class DownAttackingLinkState : ILinkActionState
    {
        private Link link;

        public DownAttackingLinkState(Link link)
        {
            this.link = link;
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    break;
                case RightFacingLinkState:
                    break;
                case UpFacingLinkState:
                    break;
                case DownFacingLinkState:
                    break;
            }
            List<Rectangle> rects = new List<Rectangle>
                {
                    new Rectangle(69, 11, 16, 16),
                    new Rectangle(86, 11, 16, 16),
                    new Rectangle(103, 11, 16, 16),
                    new Rectangle(120, 11, 16, 16)
                };
            link.linkSprite = new MovingAnimatedSprite(link.x, link.y, rects, false, 15); // non-moving?
        }

        public void BeIdle()
        {
            link.actionState = new IdleActionState(link);
        }

        public void BeMoving()
        {
            link.actionState = new MovingActionState(link);
        }

        public void Update()
        {
            link.Attack();
        }
    }
}
