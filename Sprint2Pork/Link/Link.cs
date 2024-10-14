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
    public class Link
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

        private int damageEffectCounter;
        private bool isTakingDamage;
        private const int flashRate = 5;


        int attackFrameCount;
        public bool itemInUse;

        public Link(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            directionState = new DownFacingLinkState(this);
            actionState = new IdleActionState(this);
            itemInUse = false;
            x = 50;
            y = 50;
            offsetX = 0;
            offsetY = 0;
            attackFrameCount = 0;
            frozen = false;

            // Initialize damage effect fields
            this.damageEffectCounter = 0;
            this.isTakingDamage = false;
        }

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

        public void TakeDamage()
        {
            actionState.TakeDamage();
        }

        public void loseItem()
        {
            itemInUse = false;
            linkItem = new NoItem();
        }

        // METHOD BODIES
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

        public void Move(bool reverse)
        {
            int stepLength = 4;
            if (reverse)
            {
                stepLength = -8;
            }
            switch (directionState)
            {
                case LeftFacingLinkState:
                    if (x > 0)
                    {
                        this.x = this.x - stepLength;
                    }
                    break;
                case RightFacingLinkState:
                    if (x < screenWidth)
                    {
                        this.x = this.x + stepLength;
                    }
                    break;
                case UpFacingLinkState:
                    if (y > 0)
                    {
                        this.y = this.y - stepLength;
                    }
                    break;
                case DownFacingLinkState:
                    if (y < screenHeight)
                    {
                        this.y = this.y + stepLength;
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
            if (index == 0)
            {
                linkItem = new Sword(this);
            }
            else if (index == 1)
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
            else if(index == 4)
            {
                linkItem = new WoodArrow(this);
            }
            else if(index == 5)
            {
                linkItem = new BlueBoomer(this);
            }
            else if(index == 6)
            {
                linkItem = new Fire(this);
            }
        }

        public bool BeDamaged()
        {
            isTakingDamage = true;
            if (damageEffectCounter % flashRate == 0)
            {
                if ((damageEffectCounter / flashRate) % 2 == 0)
                {
                    this.TakeDamage();
                }
                else
                {
                    this.BeIdle();
                }
                this.Move(true);
            }

            damageEffectCounter++;
            if (damageEffectCounter >= flashRate * 10)
            {
                isTakingDamage = false;
                damageEffectCounter = 0;
            }

            return isTakingDamage;
        }

        public void UpdateItem()
        {
            itemInUse = true;
            linkCount++;

            if (linkCount > 20)
            {
                linkCount = 0;
                itemInUse = false;
                offsetY = 0;
                offsetX = 0;
                loseItem();
            }
        }

        public Rectangle getRect() {
            return linkSprite.getRect();
        }

        public Rectangle getItemRect() {
            return linkItemSprite.getRect();
        }
    }
}