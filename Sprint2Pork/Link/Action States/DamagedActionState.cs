﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class DamagedActionState : ILinkActionState
    {
        private Link link;
        private bool isFlashing;

        public DamagedActionState(Link link, bool isFlashing)
        {
            this.link = link;
            this.isFlashing = isFlashing;
            SetFlashingSprite();


        }

        private void SetFlashingSprite()
        {
            List<Rectangle> rects = new List<Rectangle>();
            bool flipped = false;

            switch (link.directionState)
            {
                case LeftFacingLinkState:
                    if (isFlashing)
                    {
                        rects.Add(new Rectangle(34, 129, 16, 16)); // Flash frame
                        rects.Add(new Rectangle(50, 129, 16, 16));
                    }
                    else
                    {
                        rects.Add(new Rectangle(34, 0, 16, 15)); // Normal frame
                        rects.Add(new Rectangle(50, 0, 16, 16));
                    }
                    flipped = true;
                    break;
                case RightFacingLinkState:
                    if (isFlashing)
                    {
                        rects.Add(new Rectangle(34, 129, 16, 16)); // Flash frame
                        rects.Add(new Rectangle(50, 129, 16, 16));
                    }
                    else
                    {
                        rects.Add(new Rectangle(34, 0, 16, 15)); // Normal frame
                        rects.Add(new Rectangle(50, 0, 16, 16));
                    }
                    break;
                case UpFacingLinkState:
                    if (isFlashing)
                    {
                        rects.Add(new Rectangle(100, 129, 16, 16)); // Flash frame
                        rects.Add(new Rectangle(116, 129, 16, 16));
                    }
                    else
                    {
                        rects.Add(new Rectangle(100, 0, 16, 16)); // Normal frame
                        rects.Add(new Rectangle(116, 0, 16, 16));
                    }
                    break;
                case DownFacingLinkState:
                    if (isFlashing)
                    {
                        rects.Add(new Rectangle(0, 129, 16, 16)); // Flash frame
                        rects.Add(new Rectangle(16, 129, 16, 16));
                    }
                    else
                    {
                        rects.Add(new Rectangle(0, 0, 16, 15)); // Normal frame
                        rects.Add(new Rectangle(16, 0, 16, 15));
                    }
                    break;
            }

            link.LinkSpriteSet(new MovingAnimatedSprite(link.GetX(), link.GetY(), rects, flipped, 8, "Damaged"));
        }

        public void BeIdle()
        {
            //link.actionState = new IdleActionState(link);
        }

        public void BeMoving()
        {
            //link.actionState = new MovingActionState(link);
        }

        public void BeAttacking()
        {
            //link.actionState = new AttackingActionState(link);
        }

        public void TakeDamage()
        {
            // NO-OP
        }

        public void Update()
        {
            link.BeDamaged();
        }
    }
}
