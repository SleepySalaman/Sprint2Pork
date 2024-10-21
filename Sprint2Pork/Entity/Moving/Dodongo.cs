using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Dodongo : Enemy
    {
        private int x, y;

        private int moveX = 0;
        private int moveY = 0;

        private int moveDistance = 50;
        private int movedAmount = 0;

        private int direction = 0;

        private bool moving = false;

        private List<Rectangle> upRects;
        private List<Rectangle> rightRects;
        private List<Rectangle> damagedRightRects;
        private List<Rectangle> leftRects;
        private List<Rectangle> damagedLeftRects;
        private List<Rectangle> downRects;

        public Dodongo(int initX, int initY){
            sourceRects = new List<Rectangle>();

            x = initX;
            y = initY;

            upRects = new List<Rectangle>() {
                new Rectangle(60, 80, 24, 32),
                new Rectangle(60, 110, 24, 32)
            };

            rightRects = new List<Rectangle>() {
                new Rectangle(84, 80, 32, 32),
                new Rectangle(84, 110, 32, 32)
            };

            damagedRightRects = new List<Rectangle>()
            {

            };

            leftRects = new List<Rectangle>() {
                new Rectangle(24, 80, 32, 32),
                new Rectangle(24, 110, 32, 32)
            };

            damagedLeftRects = new List<Rectangle>()
            {

            };

            downRects = new List<Rectangle>() {
                new Rectangle(0, 80, 24, 32),
                new Rectangle(0, 110, 24, 32)
            };

            totalFrames = sourceRects.Count;
            maxCount = 5;

            destinationRect = new Rectangle(initX, initY, 100, 100);
        }

        public override void Move(List<Block> blocks){
            if (!moving){
                moving = true;
                direction = new Random().Next(1, 5);
                switch (direction){
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
            } else {
                movedAmount++;
                if (movedAmount > moveDistance){
                    moving = false;
                    movedAmount = 0;
                } else { 
                    switch (direction){
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
            foreach(Block b in blocks) {
               if(Collision.Collides(destinationRect, b.getBoundingBox())) {
                    movedAmount = 0;
                    moving = false;
                    switch (direction) {
                        case 1:
                            moveX -= 2;
                            destinationRect.X -= 2;
                            break;
                        case 2:
                            moveX += 2;
                            destinationRect.X += 2;
                            break;
                        case 3:
                            moveY += 2;
                            destinationRect.Y += 2;
                            break;
                        case 4:
                            moveY -= 2;
                            destinationRect.Y -= 2;
                            break;
                    }
                }
            }
        }
    }
}
