﻿using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Aquamentus : Enemy
    {
        private int x;

        private int moveCount = 0;
        private int moveMaxCount = 1;
        private int moveDist = 1;

        private bool movingRight = true;

        public Aquamentus(int initX, int initY, int id)
        {
            sourceRects = new List<Rectangle>() {
                new Rectangle(36, 0, 36, 36),
                new Rectangle(84, 0, 36, 36)
            };

            x = initX;

            fireballID = id;

            health = 4;

            totalFrames = sourceRects.Count;

            collisionRect = new Rectangle(initX + 35, initY, rectW - 40, rectH - 10);
            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move(List<Block> blocks)
        {
            moveCount++;
            if (moveCount > moveMaxCount)
            {
                moveCount = 0;
                if (movingRight && relativeX < 50)
                {
                    relativeX += moveDist;
                }
                else if (!movingRight && relativeX > -50)
                {
                    relativeX -= moveDist;
                }
                else
                {
                    movingRight = !movingRight;
                }
            }
            destinationRect.X = x + relativeX;
            collisionRect.X = destinationRect.X + 35;
        }

        public override int getTextureIndex() { return 2; }

        public void CurseActivate()
        {
            moveDist = 2;
        }

    }
}
