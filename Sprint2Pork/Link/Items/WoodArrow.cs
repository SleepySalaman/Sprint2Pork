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
    public class WoodArrow : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        int startX = 0;
        int startY = 0;
        public WoodArrow(Link link)
        {
            string directionStr = "Down";
            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Right";
                    startX = -50;
                    startY = 50;
                    rect = new Rectangle(1, 29, 6, 18); // 7 47
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Left";
                    startX = 125;
                    startY = 30;
                    rect = new Rectangle(1, 29, 6, 18);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Up";
                    startX = 50;
                    startY = 133;
                    rect = new Rectangle(1, 29, 6, 18);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    startX = 25;
                    startY = -60;
                    rect = new Rectangle(1, 29, 6, 18);
                    break;
            }
            link.linkItemSprite = new MovingNonAnimatedSprite(link.x + link.offsetX + startX, link.y + link.offsetY + startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.offsetX -= 7;
            }
            else if (direction == 1)
            {
                link.offsetX += 7;
            }
            else if (direction == 2)
            {
                link.offsetY += 7;
            }
            else if (direction == 3)
            {
                link.offsetY -= 7;
            }
            link.UpdateItem();
        }

        public int getDirection()
        {
            return direction;
        }
    }
}