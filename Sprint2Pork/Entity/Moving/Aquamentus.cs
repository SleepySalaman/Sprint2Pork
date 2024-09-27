using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Aquamentus : Entity {

        public Aquamentus() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(36, 0, 36, 36),
                new Rectangle(84, 0, 36, 36)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

    }
}
