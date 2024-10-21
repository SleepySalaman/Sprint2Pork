using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata.Ecma335;

namespace Sprint2Pork
{
    // TODO: Do methods inside need to be public?
    public class Link
    {
        public ILinkDirectionState directionState;
        public ILinkActionState actionState;
        public ILinkItems linkItem;

        private int X;
        private int Y;
        private int OffsetX;
        private int OffsetY;

        private int screenWidth;
        private int screenHeight;
        public ISprite linkSprite;
        public ISprite linkItemSprite;

        private bool isMoving;
        public bool IsFrozen;
        public bool IsDamaged;
        private int damageEffectCounter;
        private bool isTakingDamage;
        private const int flashRate = 5;

        int attackFrameCount;
        public bool ItemInUse;
        private int linkCount;

        public Link(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            directionState = new DownFacingLinkState(this);
            actionState = new IdleActionState(this);
            ItemInUse = false;
            X = 115;
            Y = 180;
            OffsetX = 0;
            OffsetY = 0;
            attackFrameCount = 0;
            IsFrozen = false;

            // Initialize damage effect fields
            this.damageEffectCounter = 0;
            this.isTakingDamage = false;
        }

        public int GetX() => this.X;
        public void SetX(int newX) => this.X = newX;

        public int GetY() => this.Y;
        public void SetY(int newY) => this.Y = newY;

        public int OffsetXGet() => this.OffsetX;
        public void OffsetXSet(int newX) => this.OffsetX = newX;
        public void OffsetXChange(int change) => this.OffsetX = this.OffsetX + change;
        public int OffsetYGet() => this.OffsetY;
        public void OffsetYSet(int newY) => this.OffsetY = newY;
        public void OffsetYChange(int change) => this.OffsetY = this.OffsetY + change;
        
        public int LinkCountGet() => this.linkCount;
        public void LinkCountSet(int count) => this.linkCount = count;

        public void LookLeft()
        {
            if (!isTakingDamage)
            {
                directionState.LookLeft();
            }
        }

        public void LookRight()
        {
            if (!isTakingDamage)
            {
                directionState.LookRight();
            }
        }

        public void LookUp()
        {
            if (!isTakingDamage)
            {
                directionState.LookUp();
            }
        }

        public void LookDown()
        {
            if (!isTakingDamage)
            {
                directionState.LookDown();
            }
        }

        public void BeIdle() => actionState.BeIdle();
        public void BeMoving() => actionState.BeMoving();
        public void BeAttacking() => actionState.BeAttacking();
        public void TakeDamage() => actionState.TakeDamage();

        public void loseItem()
        {
            ItemInUse = false;
            linkItem = new NoItem();
        }

        public void Draw(SpriteBatch sb, Texture2D texture, Texture2D itemTexture)
        {
            linkSprite.Draw(sb, texture);
            if (ItemInUse)
            {
                linkItemSprite.Draw(sb, itemTexture);
            }
        }

        public void Idle() => isMoving = false;

        public void Move(bool reverse)
        {
            int stepLength = reverse ? -8 : 4;

            switch (directionState)
            {
                case LeftFacingLinkState:
                    if (X > 0)
                    {
                        this.X -= stepLength;
                    }
                    break;
                case RightFacingLinkState:
                    if (X < screenWidth)
                    {
                        this.X += stepLength;
                    }
                    break;
                case UpFacingLinkState:
                    if (Y > 0)
                    {
                        this.Y -= stepLength;
                    }
                    break;
                case DownFacingLinkState:
                    if (Y < screenHeight)
                    {
                        this.Y += stepLength;
                    }
                    break;
            }
        }

        public void Attack()
        {
            IsFrozen = true;
            attackFrameCount++;

            if (attackFrameCount > 40)
            {
                attackFrameCount = 0;
                IsFrozen = false;
                this.BeIdle();
            }
        }

        public void UseItem(int index)
        {
            ItemInUse = true;
            linkItem = index switch
            {
                0 => new Sword(this),
                1 => new Arrow(this),
                2 => new Boomerang(this),
                3 => new Bomb(this),
                4 => new WoodArrow(this),
                5 => new BlueBoomer(this),
                6 => new Fire(this),
                _ => linkItem
            };
        }

        public bool BeDamaged()
        {
            isTakingDamage = true;
            bool flash = (damageEffectCounter % 2 == 0) ? true : false;
            actionState = new DamagedActionState(this, flash);
            this.Move(true);

            damageEffectCounter++;
            if (damageEffectCounter >= flashRate * 10)
            {
                isTakingDamage = false;
                damageEffectCounter = 0;
                actionState = new IdleActionState(this);
            }
            return isTakingDamage;
        }

        public void UpdateItem()
        {
            ItemInUse = true;
            linkCount++;

            if (linkCount > 20)
            {
                linkCount = 0;
                ItemInUse = false;
                OffsetY = 0;
                OffsetX = 0;
                loseItem();
            }
        }

        public Rectangle GetRect() => linkSprite.GetRect();

        public Rectangle GetItemRect() => linkItemSprite.GetRect();

    }
}