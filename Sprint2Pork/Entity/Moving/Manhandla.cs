using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Manhandla : Enemy
    {

        private int moveX = 0;
        private int moveY = 0;

        private int moveDistance = 25;
        private int movedAmount = 0;

        private int direction = 0;

        private int x;
        private int y;

        private bool moving = false;

        public Manhandla(int initX, int initY){
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 199, 60, 50),
                new Rectangle(60, 200, 60, 50),
                new Rectangle(120, 200, 60, 50),
                new Rectangle(180, 199, 60, 50),
                new Rectangle(240, 199, 60, 50),
                new Rectangle(0, 255, 60, 50),
                new Rectangle(60, 256, 60, 50),
                new Rectangle(120, 255, 60, 50),
                new Rectangle(180, 255, 60, 50)
            };

            x = initX;
            y = initY;

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
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
            destinationRect.X = x + moveX;
            destinationRect.Y = y + moveY;
        }

    }
}
