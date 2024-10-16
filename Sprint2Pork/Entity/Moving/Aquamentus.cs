using Microsoft.Xna.Framework;
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

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move()
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
        }
    }
}
