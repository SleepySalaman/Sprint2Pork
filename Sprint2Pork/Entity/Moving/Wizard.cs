﻿using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Wizard : Enemy
    {

        public Wizard(int initX, int initY)
        {
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 0, 16, 16)
            };

            health = 1;
            totalFrames = sourceRects.Count;

            collisionRect = new Rectangle(initX, initY, rectW / 2, rectH / 2);
            destinationRect = new Rectangle(initX, initY, rectW / 2, rectH / 2);
        }

        public override void Move(List<Block> blocks)
        {

        }

        public override int getTextureIndex() { return 6; }

    }
}
