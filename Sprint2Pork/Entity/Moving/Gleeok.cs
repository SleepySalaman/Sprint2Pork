using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Gleeok : Enemy
    {

        public Gleeok(int initX, int initY){
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 38, 34, 44),
                new Rectangle(35, 37, 34, 44),
                new Rectangle(70, 37, 34, 44)
            };

            totalFrames = sourceRects.Count;

            collisionRect = new Rectangle(initX, initY, rectW, rectH);
            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move(List<Block> blocks)
        {

        }

        public override int getTextureIndex() { return 2; }

    }
}
