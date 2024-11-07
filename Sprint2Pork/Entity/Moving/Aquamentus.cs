using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Aquamentus : Enemy
    {
        private int x;

        private bool fireballsCreated;

        private int fireballCount = 3;
        private int fireballDistance = -40;

        private List<Fireball> fireballs;

        private int moveCount = 0;
        private int moveMaxCount = 1;

        private bool movingRight = true;

        public Aquamentus(int initX, int initY){
            sourceRects = new List<Rectangle>() {
                new Rectangle(36, 0, 36, 36),
                new Rectangle(84, 0, 36, 36)
            };

            x = initX;

            fireballs = new List<Fireball>();

            for (int i = 0; i < fireballCount; i++)
            {
                fireballs.Add(new Fireball(i, fireballDistance * i, initX, initY));
            }

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
                    relativeX++;
                }
                else if (!movingRight && relativeX > -50)
                {
                    relativeX--;
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

    }
}
