using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class LeftMovingLinkState : ILinkState
    {
        private Link link;
        public LeftMovingLinkState(Link link)
        {
            this.link = link;
        }

        public void changeDirection()
        {
            //TODO
        }

        public void Update()
        {
            link.MoveLeft();
        }
    }
}
