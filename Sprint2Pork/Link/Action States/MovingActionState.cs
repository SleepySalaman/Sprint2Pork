using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class MovingActionState : ILinkActionState
    {
        private Link link;

        public MovingActionState(Link link) { 

            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    link.directionState = new LeftFacingLinkState(link);
                    break;
                    case RightFacingLinkState:
                    link.directionState = new RightFacingLinkState(link);
                    break;
                case UpFacingLinkState:
                    link.directionState = new UpFacingLinkState(link);
                    break;
                case DownFacingLinkState:
                    link.directionState = new DownFacingLinkState(link);
                    break;
            }
            this.link = link;
        }

        public void BeIdle()
        {
            link.actionState = new IdleActionState(link);
        }

        public void BeMoving()
        {
            // NO-OP
        }

        public void BeAttacking()
        {
            link.actionState = new AttackingActionState(link);
        }

        public void TakeDamage()
        {
            link.actionState = new DamagedActionState(link, 10);
        }

        public void Update()
        {
            link.Move(false);
        }
    }
}
