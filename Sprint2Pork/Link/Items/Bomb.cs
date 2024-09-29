﻿using Microsoft.Xna.Framework;
using System;

namespace Sprint2Pork
{
    public class Bomb : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        string directionStr = "Down";
        public Bomb(Link link)
        {
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    link.offsetX = -50;
                    link.offsetY = 15;
                    rect = new Rectangle(127, 29, 9, 16); //136 45
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    link.offsetX = 87;
                    link.offsetY = 15;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    link.offsetX = 25;
                    link.offsetY = 75;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    link.offsetX = 25;
                    link.offsetY = -75;
                    rect = new Rectangle(127, 29, 9, 16);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX, link.y + link.offsetY, rect, directionStr);
        }

        public void Update(Link link)
        {
            switch (direction)
            {
                case 1:
                    link.offsetX = -10;
                    break;
                case 2:
                    link.offsetX = 10;
                    break;
                case 3:
                    link.offsetY = 10;
                    break;
                case 4:
                    link.offsetY = -10;
                    break;
            }

            //Bomb Explosion
            if (link.linkCount >= 18){
                rect = new Rectangle(153, 29, 17, 28); // 170 47
                if (link.directionState is RightFacingLinkState)
                {
                    link.offsetX += 87;
                }
                else if(link.directionState is UpFacingLinkState)
                {
                    link.offsetY -= 87;
                    link.offsetX -= 25;
                }
                else if(link.directionState is LeftFacingLinkState)
                {
                    link.offsetX -= 15;
                }
                link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX, link.y + link.offsetY, rect, directionStr);
            }
            link.UpdateItem();
        }

        public int getDirection()
        {
            return direction;
        }
    }
}