using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint2Pork
{
    // TODO: Do methods inside need to be public?
    public class Link : ILink
    {
        public ILinkDirectionState directionState;
        public ILinkActionState actionState;
        public ILinkItems linkItem;
        //private Viewport viewport;
        public int x;
        public int y;
        private int screenWidth;
        private int screenHeight;
        public ISprite linkSprite;
        public ISprite linkItemSprite;

        private bool moving;
        public bool frozen;
        public bool damage;
        public int linkCount;
        

        int attackFrameCount;

        public Link(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            directionState = new DownFacingLinkState(this);
            actionState = new IdleActionState(this);
            linkItem = new IdleItem(this);
            x = 0;
            y = 0;
            attackFrameCount = 0;
            frozen = false;
        }

        //public void TakeDamage()
        //{
        //    state.TakeDamage();
        //}

        //public void UseItem()
        //{
        //    state.UseItem();
        //}

        //public void AttackSword()
        //{
        //    state.AttackSword();
        //}

        public void LookLeft()
        {
            directionState.LookLeft();
        }

        public void LookRight()
        {
            directionState.LookRight();
        }

        public void LookUp()
        {
            directionState.LookUp();
        }

        public void LookDown()
        {
            directionState.LookDown();
        }

        //public void ChangeDirection()
        //{
        //    switch (directionState)
        //    {
        //        case LeftFacingLinkState:
        //            break;
        //        case RightFacingLinkState:
        //            break;
        //        case UpFacingLinkState:
        //            break;
        //        case DownFacingLinkState:
        //            break;
        //    }
        //}

        public void BeIdle()
        {
            actionState.BeIdle();
        }

        public void BeMoving()
        {
            actionState.BeMoving();
        }

        public void BeAttacking()
        {
            actionState.BeAttacking();
        }

        public void loseItem()
        {
            linkItem = new IdleItem(this);
        }
        // ACTUAL METHODS

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            linkSprite.Draw(sb, texture);
        }

        public void Idle()
        {
            moving = false;
        }

        public void Move()
        {

            switch (directionState)
            {
                case LeftFacingLinkState:
                    if (x > 0)
                    {
                        this.x = this.x - 3;

                    }
                    break;
                case RightFacingLinkState:
                    if (x < screenWidth)
                    {
                        this.x = this.x + 3;
                    }
                    break;
                case UpFacingLinkState:
                    if (y > 0)
                    {
                        this.y = this.y - 3;
                    }
                    break;
                case DownFacingLinkState:
                    if (y < screenHeight)
                    {
                        this.y = this.y + 3;
                    }
                    break;
            }


        }
        public void Attack()
        {
            //actionState = new AttackingActionState(this);
            frozen = true;
            attackFrameCount++;
            if (attackFrameCount > 40)
            {
                attackFrameCount = 0;
                frozen = false;
                this.BeIdle();
            }

        }

        public void UseItem(int index)
        {
            if(index == 1)
            {
                linkItem = new Arrow(this);
            }
        }

        public void UseArrow()
        {
            frozen = true;
            linkCount++;

            if (linkCount > 40)
            {
                linkCount = 0;
                frozen = false;
                loseItem();
            }
        }
    }
}