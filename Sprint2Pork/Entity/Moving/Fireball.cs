using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Fireball : Entity {

        public Fireball() {
            sourceRects = new List<Rectangle>() {
                new Rectangle(101, 14, 8, 10),
                new Rectangle(110, 14, 8, 10),
                new Rectangle(119, 14, 8, 10),
                new Rectangle(128, 14, 8, 10)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX - 100, initY - 100, 20, 20);
        }
    }
}
