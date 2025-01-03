﻿using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Gohma : Enemy
    {

        public Gohma(int initX, int initY)
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

            health = 4;

            totalFrames = sourceRects.Count;

            collisionRect = new Rectangle(initX, initY + 40, rectW - 20, rectH - 45);
            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move(List<Block> blocks)
        {

        }

        public override int getTextureIndex() { return 2; }

    }
}
