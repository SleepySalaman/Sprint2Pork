﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class Arrow : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        string directionStr = "Down";
        int startX = 0;
        int startY = 0;
        public Arrow(Link link)
        {
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Right";
                    startX = -50;
                    startY = 50;
                    rect = new Rectangle(27, 30, 7, 18); // 34 48
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Left";
                    startX = 125;
                    startY = 30;
                    rect = new Rectangle(27, 30, 7, 18);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Up";
                    startX = 50;
                    startY = 133; 
                    rect = new Rectangle(27, 30, 7, 18);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    startX = 25;
                    startY = -60;
                    rect = new Rectangle(27, 30, 7, 18);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX + startX, link.y + link.offsetY + startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.offsetX -= 12;
            }
            else if (direction == 1)
            {
                link.offsetX += 12;
            }
            else if (direction == 2)
            {
                link.offsetY += 12;
            }
            else if (direction == 3)
            {
                link.offsetY -= 12;
            }

            //Explosion
            if (link.linkCount >= 19)
            {
                rect = new Rectangle(51, 34, 10, 9); // 170 47
                if (link.directionState is RightFacingLinkState)
                {
                    link.offsetX += 87;
                    link.offsetY += 17;
                }
                else if (link.directionState is UpFacingLinkState)
                {
                    link.offsetX += 25;
                    link.offsetY -= 15;
                }
                else if (link.directionState is LeftFacingLinkState)
                {
                    link.offsetY += 50;
                }
                else if (link.directionState is DownFacingLinkState)
                {
                    link.offsetX += 50;
                    link.offsetY += 100;
                }
                link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX, link.y + link.offsetY, rect, directionStr);
            }

            link.UpdateItem();
        }
    }
}