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
        private Viewport viewport;
        public int x;
        public int y;
        public ISprite linkSprite;

        public Link()
        {
            directionState = new DownFacingLinkState(this);
            actionState = new IdleActionState(this);
            x = viewport.Width / 2;
            y = viewport.Height / 2;
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

        }

        public void Move()
        {
            switch (directionState)
            {
                case LeftFacingLinkState:
                    if (x > 0)
                    {
                        this.x = this.x - 1;

                    }
                    break;
                case RightFacingLinkState:
                    if (x > viewport.Width)
                    {
                        this.x = this.x + 1;
                    }
                    break;
                case UpFacingLinkState:
                    if (y > 0)
                    {
                        this.y = this.y - 1;
                    }
                    break;
                case DownFacingLinkState:
                    if (y > viewport.Height)
                    {
                        this.y = this.y + 1;
                    }
                    break;
            }

        }
    }
}
