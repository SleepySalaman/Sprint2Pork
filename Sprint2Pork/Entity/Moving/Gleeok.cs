using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Gleeok : Entity {

        public Gleeok() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 38, 34, 44),
                new Rectangle(35, 37, 34, 44),
                new Rectangle(70, 37, 34, 44)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

    }
}
