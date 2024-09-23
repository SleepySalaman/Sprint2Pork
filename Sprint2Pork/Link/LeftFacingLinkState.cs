using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class LeftFacingLinkState : ILinkDirectionState
    {
        private ILink link;
        public LeftFacingLinkState(ILink link)
        {
            this.link = link;
        }

        public void Update()
        {
            //link.LookLeft();
        }

        public void LookLeft()
        {
            // NO-OP
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
            //link.directionState = new DownFacingLinkState(link);
        }
    }
}
