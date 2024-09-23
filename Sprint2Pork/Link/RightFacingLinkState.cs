using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class RightFacingLinkState : ILinkDirectionState
    {
        private Link link;

        public RightFacingLinkState(Link link)
        {
            this.link = link;
        }

        public void LookLeft()
        {
            //link.directionState = new LeftFacingLinkState(link);
        }

        public void LookRight()
        {
            // NO-OP
        }

        public void LookUp()
        {
            //link.directionState = new UpFacingLinkState(link);
        }

        public void LookDown()
        {
            //link.directionState = new DownFacingLinkState(link);
        }
    }
}
