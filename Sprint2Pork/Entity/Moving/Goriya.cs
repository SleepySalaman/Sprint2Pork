using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Goriya : Enemy {

        public Goriya() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 0, 13, 16),
                new Rectangle(30, 0, 13, 16),
                new Rectangle(60, 0, 13, 16),
                new Rectangle(90, 0, 13, 16),
                new Rectangle(0, 30, 13, 16),
                new Rectangle(30, 30, 13, 16),
                new Rectangle(60, 30, 13, 16),
                new Rectangle(90, 30, 13, 16)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move() {

        }
    }
}
