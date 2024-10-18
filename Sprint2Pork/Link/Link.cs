using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    // TODO: Do methods inside need to be public?
    public class Link
    {
        public ILinkDirectionState directionState;
        public ILinkActionState actionState;
        public ILinkItems linkItem;

        public int X;
        public int Y;
        public int OffsetX;
        public int OffsetY;

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
        public int linkCount;

        public Link(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            directionState = new DownFacingLinkState(this);
            actionState = new IdleActionState(this);
            ItemInUse = false;
            X = 115;
            Y = 115;
            OffsetX = 0;
            OffsetY = 0;
            attackFrameCount = 0;
            IsFrozen = false;

            // Initialize damage effect fields
            this.damageEffectCounter = 0;
            this.isTakingDamage = false;
        }
        public void LookLeft() => directionState.LookLeft();
        public void LookRight() => directionState.LookRight();
        public void LookUp() => directionState.LookUp();
        public void LookDown() => directionState.LookDown();

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