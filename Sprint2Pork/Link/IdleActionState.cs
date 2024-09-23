using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class IdleActionState : ILinkActionState
    {
        private ILink link;

        public IdleActionState(ILink link)
        {
            this.link = link;
        }

        public void BeIdle()
        {
            // NO-OP
        }

        public void BeMoving()
        {
            //link.actionState = new MovingActionState(link);
        }

        public void Update()
        {
            //link.Idle();
        }
    }
}
