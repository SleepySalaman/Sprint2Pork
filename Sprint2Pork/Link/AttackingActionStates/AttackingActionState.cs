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
            List<Rectangle> rects = new List<Rectangle>
                {
                    new Rectangle(69, 11, 16, 16),
                    new Rectangle(86, 11, 16, 16),
                    new Rectangle(103, 11, 16, 16),
                    new Rectangle(120, 11, 16, 16)
                };
            link.linkSprite = new MovingAnimatedSprite(link.x, link.y, rects, false, 15);
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
            // Update logic for attacking
        }
    }
}
