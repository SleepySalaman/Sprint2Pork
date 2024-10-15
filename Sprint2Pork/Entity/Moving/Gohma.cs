using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Gohma : Enemy
    {

        public Gohma()
        {
            sourceRects = new List<Rectangle>() {
                new Rectangle(120, 80, 60, 30),
                new Rectangle(180, 80, 60, 30),
                new Rectangle(240, 80, 60, 30),
                new Rectangle(300, 80, 60, 30),
                new Rectangle(120, 110, 60, 30),
                new Rectangle(180, 110, 60, 30),
                new Rectangle(240, 110, 60, 30),
                new Rectangle(300, 110, 60, 30)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move()
        {

        }

    }
}
