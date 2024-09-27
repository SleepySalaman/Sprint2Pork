using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Manhandla : Enemy {

        public Manhandla() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 199, 60, 50),
                new Rectangle(60, 200, 60, 50),
                new Rectangle(120, 200, 60, 50),
                new Rectangle(180, 199, 60, 50),
                new Rectangle(240, 199, 60, 50),
                new Rectangle(0, 255, 60, 50),
                new Rectangle(60, 256, 60, 50),
                new Rectangle(120, 255, 60, 50),
                new Rectangle(180, 255, 60, 50)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move() {

        }

        public override void Attack() {

        }

    }
}
