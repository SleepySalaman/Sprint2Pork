using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Bat : Enemy {

        public Bat() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 1, 16, 8),
                new Rectangle(28, 1, 10, 9)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW / 2, rectH / 2);
        }

        public override void Move() {

        }

    }
}
