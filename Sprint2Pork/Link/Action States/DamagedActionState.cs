using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class DamagedActionState : ILinkActionState
    {
        private Link link;
        private int damageDuration;
        private int frameCounter;
        private bool isFlashing;



        public DamagedActionState(Link link, int damageDuration)
        {
            this.link = link;
            this.damageDuration = damageDuration;
            this.frameCounter = 0;
            this.isFlashing = false;
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

            link.linkSprite = new MovingAnimatedSprite(link.X, link.Y, rects, flipped, 8, "Damaged");
        }

        public void BeIdle()
        {
            link.actionState = new IdleActionState(link);
        }

        public void BeMoving()
        {
            link.actionState = new MovingActionState(link);
        }

        public void BeAttacking()
        {
            link.actionState = new AttackingActionState(link);
        }

        public void TakeDamage()
        {
            // NO-OP
        }

        public void Update()
        {
            frameCounter++;

            if (frameCounter % 10 != 0)
            {
                isFlashing = !isFlashing;
                SetFlashingSprite();
            }

            if (frameCounter >= damageDuration)
            {
                BeIdle();
            }
        }
    }
}
