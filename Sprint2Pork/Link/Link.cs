using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Essentials;
using Sprint2Pork.Items;
using Sprint2Pork.Managers;
using System;
using System.Collections.Generic;


namespace Sprint2Pork
{
    // TODO: Do methods inside need to be public?
    public class Link
    {
        public ILinkDirectionState directionState;
        public ILinkActionState actionState;
        public ILinkItems linkItem;
        private ILinkDirectionState frozenDirectionState;
        private int X;
        private int Y;
        private int OffsetX;
        private int OffsetY;

        private int screenWidth;
        private int screenHeight;
        private ISprite linkSprite;

        private int damageEffectCounter;
        private bool isTakingDamage;
        private const int flashRate = 1;

        int attackFrameCount;
        private bool ItemInUse;
        private int linkCount;

        private SoundManager soundManager;
        private SoundEffectInstance soundInstance;
        private bool playingFlag;
        private Inventory inventory;
        private List<String> items;
        private int currentItemIndex;
        public event Action SlotBChanged;

        public bool isInvincible;
        private readonly double invincibilityDuration;
        private double invincibilityTimer;

        public String SlotA { get; private set; }
        public String SlotB { get; private set; }

        public Link(int width, int height, SoundManager paramSoundManager, Inventory inventory)
        {
            screenWidth = width;
            screenHeight = height;
            directionState = new DownFacingLinkState(this);
            actionState = new IdleActionState(this);
            linkItem = new NoItem();
            ItemInUse = false;
            X = 115;
            Y = 180;
            OffsetX = 0;
            OffsetY = 0;
            attackFrameCount = 0;
            this.inventory = inventory;

            // Initialize damage effect and temp invincibility fields
            damageEffectCounter = 0;
            isTakingDamage = false;
            isInvincible = false;
            invincibilityDuration = 2.0;
            invincibilityTimer = 0;

            soundManager = paramSoundManager;
            playingFlag = false;

            items = new List<String>
            {
                "Sword", "Arrow", "Boomerang", "GroundBomb", "WoodArrow", "BlueBoomer", "Fire",
            };
            SlotA = "Sword";
            SlotB = items[currentItemIndex];
        }

        /*
         * Section for set/get methods for Link's private variables
         */

        public int GetX() => X;
        public void SetX(int newX) => X = newX;

        public int GetY() => Y;
        public void SetY(int newY) => Y = newY;

        public int OffsetXGet() => OffsetX;
        public void OffsetXSet(int newX) => OffsetX = newX;
        public void OffsetXChange(int change) => OffsetX = OffsetX + change;

        public int OffsetYGet() => OffsetY;
        public void OffsetYSet(int newY) => OffsetY = newY;
        public void OffsetYChange(int change) => OffsetY = OffsetY + change;

        public int LinkCountGet() => linkCount;

        public bool IsLinkUsingItem() => ItemInUse;

        public void LinkSpriteUpdate() => linkSprite.Update(X, Y);
        public void LinkSpriteSet(ISprite sprite) => linkSprite = sprite;

        /*
         * Section for non-set/get methods
         */

        public void NextItem()
        {
            currentItemIndex = (currentItemIndex + 1) % items.Count;
            SlotB = items[currentItemIndex];
            SlotBChanged?.Invoke();
        }

        public void PreviousItem()
        {
            currentItemIndex = (currentItemIndex - 1 + items.Count) % items.Count;
            SlotB = items[currentItemIndex];
            SlotBChanged?.Invoke();

        }

        public void UseItemB()
        {

            if (ItemInUse) return; // Prevent multiple uses per frame

            int itemIndex = items.IndexOf(SlotB);
            if (itemIndex != -1)
            {

                var blueArrowCount = inventory.GetItemCount("BlueGroundArrow");
                if ((itemIndex == 3 && inventory.GetItemCount("GroundBomb") > 0) ||
                    (itemIndex == 4 && inventory.GetItemCount("Rupee") > 0) ||
                    (itemIndex == 1 && inventory.GetItemCount("Rupee") > 0) ||
                    (itemIndex != 3 && itemIndex != 1 && itemIndex != 4))
                {
                    UseItem(itemIndex);
                }
            }
        }

        public void PlaySound(string soundName)
        {
            if (!playingFlag)
            {
                SoundEffect sound = soundManager.GetSound(soundName);
                soundInstance = sound.CreateInstance();
                soundInstance.Volume = 0.1f;
                soundInstance.Play();
                playingFlag = true;
            }
            else
            {
                if (soundInstance.State != SoundState.Playing)
                {
                    playingFlag = false;
                }
            }


        }

        public void LookLeft()
        {
            if (!isTakingDamage && !ItemInUse)  // Prevent direction change when item is in use
            {
                directionState.LookLeft();
            }
        }

        public void LookRight()
        {
            if (!isTakingDamage && !ItemInUse)
            {
                directionState.LookRight();
            }
        }

        public void LookUp()
        {
            if (!isTakingDamage && !ItemInUse)
            {
                directionState.LookUp();
            }
        }

        public void LookDown()
        {
            if (!isTakingDamage && !ItemInUse)
            {
                directionState.LookDown();
            }
        }


        public void BeIdle() => actionState.BeIdle();
        public void BeMoving() => actionState.BeMoving();
        public void BeAttacking() => actionState.BeAttacking();
        public void TakeDamage()
        {
            if (!isInvincible)
            {
                damageEffectCounter = 0;
                isTakingDamage = true;
                isInvincible = true;
                invincibilityTimer = invincibilityDuration;
                actionState.TakeDamage();
            }
        }

        public void UpdateInvincibilityTimer(GameTime gameTime)
        {
            if (isInvincible)
            {
                invincibilityTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibilityTimer <= 0)
                {
                    isInvincible = false;
                    invincibilityTimer = 0;
                }
            }
        }

        public void LoseItem()
        {
            ItemInUse = false;
            linkItem = new NoItem();
            frozenDirectionState = null;
            attackFrameCount = 40;
        }


        public void Draw(SpriteBatch sb, Texture2D texture, Texture2D itemTexture)
        {
            linkSprite.Draw(sb, texture);
            if (ItemInUse)
            {
                linkItem.Draw(sb, itemTexture);
            }
        }

        public void Move(bool reverse)
        {
            if (ItemInUse)  // Keep using the frozen direction when item is in use
            {
                directionState = frozenDirectionState;
            }

            int stepLength = reverse ? -8 : 4;

            switch (directionState)
            {
                case LeftFacingLinkState:
                    if (X > 0)
                    {
                        X -= stepLength;
                    }
                    break;
                case RightFacingLinkState:
                    if (X < screenWidth)
                    {
                        X += stepLength;
                    }
                    break;
                case UpFacingLinkState:
                    if (Y > 0)
                    {
                        Y -= stepLength;
                    }
                    break;
                case DownFacingLinkState:
                    if (Y < screenHeight)
                    {
                        Y += stepLength;
                    }
                    break;
            }
        }

        public void Attack()
        {
            attackFrameCount++;

            if (attackFrameCount > 40)
            {
                attackFrameCount = 0;
                BeIdle();
            }
        }

        public void UseItem(int index)
        {
            ItemInUse = true;
            frozenDirectionState = directionState;
            linkItem = index switch
            {
                0 => new Sword(frozenDirectionState, X, Y),
                1 => new Arrow(frozenDirectionState, X, Y),
                2 => new Boomerang(frozenDirectionState, X, Y),
                3 => new Bomb(frozenDirectionState, X, Y),
                4 => new WoodArrow(frozenDirectionState, X, Y),
                5 => new BlueBoomer(frozenDirectionState, X, Y),
                6 => new Fire(frozenDirectionState, X, Y),
                _ => linkItem
            };
            var itemToRemove = linkItem.GetItemName();
            inventory.RemoveItem(itemToRemove);
        }

        public bool BeDamaged()
        {
            isTakingDamage = true;
            bool flash = (damageEffectCounter % 2 == 0) ? true : false;
            actionState = new DamagedActionState(this, flash);
            Move(true);
            PlaySound("sfxPlayerHurt");

            damageEffectCounter++;
            if (damageEffectCounter >= flashRate * 10)
            {
                isTakingDamage = false;
                damageEffectCounter = 0;
                actionState = new IdleActionState(this);
            }
            return isTakingDamage;
        }

        public void StopLinkItem()
        {
            linkCount = 20;
            //this.UpdateItem();
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
                LoseItem();
            }
        }

        public Rectangle GetRect() => linkSprite.GetRect();

        public void CollectItem(GroundItem itemName)
        {
            inventory.AddItem(itemName);
            itemName.PerformAction();
        }
    }
}