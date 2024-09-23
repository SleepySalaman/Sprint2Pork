using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class MovingActionState : ILinkActionState
    {
        private ILink link;

        public MovingActionState(ILink link) { 
            this.link = link; 
        }

        public void BeIdle()
        {
            //link.actionState = new IdleActionState(link);
        }

        public void BeMoving()
        {
            // NO-OP
        }

        public void Update()
        {
            //link.Move();
        }
    }
}
