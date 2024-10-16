using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Goriya : Enemy
    {
        private int startX, startY;

        private int moveX = 0;
        private int moveY = 0;

        private int moveDistance = 25;
        private int movedAmount = 0;

        private int direction = 0;

        private bool moving = false;

        private List<Rectangle> downRects;
        private List<Rectangle> upRects;
        private List<Rectangle> leftRects;
        private List<Rectangle> rightRects;

        public Goriya(int initX, int initY){
            sourceRects = new List<Rectangle>();

            startX = initX;
            startY = initY;

            downRects = new List<Rectangle>() {
                new Rectangle(0, 0, 13, 16),
                new Rectangle(0, 30, 13, 16)
            };

            upRects = new List<Rectangle>() {
                new Rectangle(60, 0, 13, 16),
                new Rectangle(60, 30, 13, 16)
            };

            leftRects = new List<Rectangle>() {
                new Rectangle(30, 0, 13, 16),
                new Rectangle(30, 30, 13, 16)
            };

            rightRects = new List<Rectangle>() {
                new Rectangle(90, 0, 13, 16),
                new Rectangle(90, 30, 13, 16)
            };

            totalFrames = sourceRects.Count;
            maxCount = 5;

            destinationRect = new Rectangle(initX, initY, (rectW / 4) * 3, (rectH / 4) * 3);
        }

        public override void Move()
        {
            if (!moving)
            {
                moving = true;
                direction = new Random().Next(1, 5);
                switch (direction)
                {
                    case 1: //right
                        sourceRects = rightRects;
                        break;
                    case 2: //left
                        sourceRects = leftRects;
                        break;
                    case 3: //up
                        sourceRects = upRects;
                        break;
                    case 4: //down
                        sourceRects = downRects;
                        break;
                }
                totalFrames = sourceRects.Count;
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
            destinationRect.X = startX + moveX;
            destinationRect.Y = startY + moveY;
        }

    }
}
