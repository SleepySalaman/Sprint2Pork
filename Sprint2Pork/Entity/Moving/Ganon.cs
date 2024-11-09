using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Ganon : Enemy
    {

        public Ganon(int initX, int initY){
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 310, 40, 32),
                new Rectangle(40, 310, 40, 32),
                new Rectangle(80, 310, 40, 32),
                new Rectangle(120, 310, 40, 32),
                new Rectangle(160, 310, 40, 32)
            };

            health = 2;

            totalFrames = sourceRects.Count;

            collisionRect = new Rectangle(initX, initY, rectW, rectH);
            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move(List<Block> blocks) {

        }

        public override int getTextureIndex() { return 2; }

    }
}
