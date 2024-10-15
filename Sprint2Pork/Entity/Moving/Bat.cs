using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Bat : Enemy
    {

        private int moveX = 0;
        private int moveY = 0;

        private int moveDistance = 8;
        private int movedAmount = 0;

        private int direction = 0;

        private bool moving = false;

        public Bat()
        {
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 1, 16, 8),
                new Rectangle(28, 1, 10, 9)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW / 4, rectH / 4);
        }

        public override void Move()
        {
            if (!moving)
            {
                moving = true;
                direction = new Random().Next(1, 5);
            }
            else
            {
                movedAmount++;
                if (movedAmount > moveDistance)
                {
                    moving = false;
                    movedAmount = 0;
                }
                else
                {
                    switch (direction)
                    {
                        case 1: //right
                            moveX++;
                            break;
                        case 2: //left
                            moveX--;
                            break;
                        case 3: //up
                            moveY--;
                            break;
                        case 4: //down
                            moveY++;
                            break;
                    }
                }
            }
            destinationRect.X = initX + moveX;
            destinationRect.Y = initY + moveY;
        }
    }
}
