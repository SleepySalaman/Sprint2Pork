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
        public int offsetX;
        public int offsetY;
        private int screenWidth;
        private int screenHeight;
        public ISprite linkSprite;
        public ISprite linkItemSprite;

        private bool moving;
        public bool frozen;
        public bool damage;
        public int linkCount;
        

        int attackFrameCount;
        public bool itemInUse;

        public Link(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            directionState = new DownFacingLinkState(this);
            actionState = new IdleActionState(this);
            itemInUse = false;
            x = 0;
            y = 0;
            offsetX = 0;
            offsetY = 0;
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
            itemInUse = false;
        }
        // ACTUAL METHODS

        public void Draw(SpriteBatch sb, Texture2D texture, Texture2D itemTexture)
        {
            linkSprite.Draw(sb, texture);
            if (itemInUse)
            {
                linkItemSprite.Draw(sb, itemTexture);
            }
            
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
            itemInUse = true;
            if(index == 1)
            {
                linkItem = new Arrow(this);
            }

            else if(index == 2)
            {
                linkItem = new Boomerang(this);
            }

            else if(index == 3)
            {
                linkItem = new Bomb(this);
            }
        }

        public void AttackSword()
        {
            itemInUse = true;
            linkItem = new Sword(this);
        }

        public void UpdateItem()
        {
            itemInUse = true;
            linkCount++;

            if(linkItem.getDirection() == 0)
            {
                offsetX -= 7;
            }
            else if(linkItem.getDirection() == 1)
            {
                offsetX += 7;
            }
            else if(linkItem.getDirection() == 2)
            {
                offsetY += 7;
            }
            else if(linkItem.getDirection() == 3)
            {
                offsetY -= 7;
            }

            if (linkCount > 20)
            {
                linkCount = 0;
                itemInUse = false;
                offsetY = 0;
                offsetX = 0;
                loseItem();
            }
        }
    }
}