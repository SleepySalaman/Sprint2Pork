using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class UpFacingLinkState : ILinkDirectionState
    {
        private Link link;

        public UpFacingLinkState(Link link)
        {
            this.link = link;
        }

        public void LookLeft()
        {
            link.directionState = new LeftFacingLinkState(link);
        }

        public void LookRight()
        {
            link.directionState = new RightFacingLinkState(link);
        }

        public void LookUp()
        {
            // NO-OP
        }

        public void LookDown()
        {
            link.directionState = new DownFacingLinkState(link);
        }
    }
}
