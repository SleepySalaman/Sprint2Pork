using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Digdogger : Enemy {

        public Digdogger() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(220, 0, 40, 40),
                new Rectangle(260, 0, 40, 40)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move() {

        }
    }
}
