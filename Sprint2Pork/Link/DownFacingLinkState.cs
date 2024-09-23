using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class DownFacingLinkState : ILinkDirectionState
    {
        private ILink link;

        public DownFacingLinkState(ILink link)
        {
            this.link = link;
        }

        public void LookLeft()
        {
            //link.directionState = new LeftFacingLinkState(link);
        }

        public void LookRight()
        {
            //link.directionState = new RightFacingLinkState(link);
        }

        public void LookUp()
        {
            //link.directionState = new UpFacingLinkState(link);
        }

        public void LookDown()
        {
            // NO-OP
        }
    }
}
