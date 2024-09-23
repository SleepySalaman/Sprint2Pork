using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class DownNonMovingLinkState : ILinkState
    {
        private Link link;

        public DownNonMovingLinkState(Link link)
        {
            this.link = link;
        }

        public ChangeDirectionLeft()
        {
            this.link = new LeftNonMovingLinkState(link);
        }
    }
}
