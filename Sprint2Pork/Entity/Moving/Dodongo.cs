using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Dodongo : Enemy {

        public Dodongo() {
            sourceRects = new List<Rectangle> {
                new Rectangle(60, 80, 24, 32),
                new Rectangle(60, 110, 24, 32)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, 100, 100);
        }

        public override void Move() {

        }

    }
}
