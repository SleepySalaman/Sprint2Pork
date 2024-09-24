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
        //private Viewport viewport;
        public int x;
        public int y;
        private int screenWidth;
        private int screenHeight;
        public ISprite linkSprite;

        private bool moving;

        public Link(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            directionState = new DownFacingLinkState(this);
            actionState = new IdleActionState(this);
            x = 0;
            y = 0;
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
                    //directionState = new LeftFacingLinkState(this);
                    if (x > 0)
                    {
                        this.x = this.x - 2;

                    }
                    break;
                case RightFacingLinkState:
                    //directionState = new RightFacingLinkState(this);
                    if (x < screenWidth)
                    {
                        this.x = this.x + 2;
                    }
                    break;
                case UpFacingLinkState:
                    //directionState = new UpFacingLinkState(this);
                    if (y > 0)
                    {
                        this.y = this.y - 2;
                    }
                    break;
                case DownFacingLinkState:
                    //directionState = new DownFacingLinkState(this);
                    if (y < screenHeight)
                    {
                        this.y = this.y + 2;
                    }
                    break;
            }


        }
        public void Attack()
        {
            switch (directionState)
            {
                case UpFacingLinkState:
                    actionState = new UpAttackingLinkState(this);
                    break;
                case DownFacingLinkState:
                    actionState = new AttackingActionState(this);
                    break;
                case LeftFacingLinkState:
                    actionState = new LeftAttackingLinkState(this);
                    break;
                case RightFacingLinkState:
                    actionState = new RightAttackingLinkState(this);
                    break;
            }
        }
    }
}