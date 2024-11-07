using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Bat : Enemy
    {
        private int x, y;

        private int moveX = 0;
        private int moveY = 0;

        private int moveDistance = 8;
        private int movedAmount = 0;

        private int direction = 0;

        private bool moving = false;

        public Bat(int initX, int initY){
            sourceRects = new List<Rectangle>() {
                new Rectangle(0, 1, 16, 8),
                new Rectangle(28, 1, 10, 9)
            };

            x = initX;
            y = initY;

            totalFrames = sourceRects.Count;

            collisionRect = new Rectangle(initX, initY, rectW / 4, rectH / 4);
            destinationRect = new Rectangle(initX, initY, rectW / 4, rectH / 4);
        }

        public override void Move(List<Block> blocks)
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
            foreach (Block b in blocks) {
                if (Collision.Collides(destinationRect, b.getBoundingBox())) {
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
            if(moving && Collision.CollidesWithOutside(destinationRect, roomBoundingBox)) {
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
            collisionRect.X = destinationRect.X;
            collisionRect.Y = destinationRect.Y;
        }

        public override int getTextureIndex() { return 4; }

    }
}
