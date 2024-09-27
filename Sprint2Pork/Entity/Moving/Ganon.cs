using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Ganon : Entity {

        public Ganon() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 310, 40, 32),
                new Rectangle(40, 310, 40, 32),
                new Rectangle(80, 310, 40, 32),
                new Rectangle(120, 310, 40, 32),
                new Rectangle(160, 310, 40, 32)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

    }
}
