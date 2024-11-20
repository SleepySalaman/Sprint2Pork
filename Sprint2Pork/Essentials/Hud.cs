using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Essentials;
using System;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class HUD
    {
        private Inventory inventory;
        private SpriteFont font;
        private String slotAItem;
        private String slotBItem;
        private int currentRoomNumber;
        private Link link;
        private Dictionary<string, Rectangle> itemSourceRects;

        public HUD(Inventory inventory, SpriteFont font, Link link)
        {
            this.inventory = inventory;
            this.font = font;
            this.slotAItem = link.SlotA;
            this.slotBItem = link.SlotB;
            this.link = link;
            InitializeHUDItemSourceRects();

            link.SlotBChanged += OnSlotBChanged;
        }

        private void OnSlotBChanged()
        {
            slotBItem = link.SlotB;
        }

        public void SubscribeToLinkEvents(Link link)
        {
            this.link = link;
            slotBItem = link.SlotB;
            link.SlotBChanged += OnSlotBChanged;
        }

        public void InitializeHUDItemSourceRects()
        {
            itemSourceRects = new Dictionary<string, Rectangle>
            {
                { "Rupee", new Rectangle(72, 0, 8, 16) },
                { "Key", new Rectangle(240, 0, 8, 16) },
                { "GroundBomb", new Rectangle(136, 0, 8, 16) },
                { "Sword", new Rectangle(104, 16, 6, 16) },
                { "Arrow", new Rectangle(152, 16, 8, 16) },
                { "Boomerang", new Rectangle(128, 0, 8, 16) },
                { "WoodArrow", new Rectangle(152, 0, 8, 16) },
                { "BlueBoomer", new Rectangle(128, 16, 8, 16) },
                { "Fire", new Rectangle(224, 0, 8, 16) },
            };
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D itemsTexture, float scale = 2.0f)
        {
            // Draw inventory counts
            spriteBatch.DrawString(font, $"{inventory.GetItemCount("Rupee")}", new Vector2(335, 10), Color.White);
            spriteBatch.DrawString(font, $"{inventory.GetItemCount("Key")}", new Vector2(335, 35), Color.White);
            spriteBatch.DrawString(font, $"{inventory.GetItemCount("GroundBomb")}", new Vector2(335, 60), Color.White);

            // Draw current room number
            if (currentRoomNumber != -1)
            {
                spriteBatch.DrawString(font, $"{currentRoomNumber}", new Vector2(250, 11), Color.White);
            }

            // Draw Slot A item sprite
            if (!string.IsNullOrEmpty(slotAItem) && itemSourceRects.ContainsKey(slotAItem))
            {
                Rectangle sourceRect = itemSourceRects[slotAItem];
                Vector2 slotAPosition = new Vector2(445, 40);
                spriteBatch.Draw(itemsTexture, slotAPosition, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            else
            {
                Rectangle sourceRect = itemSourceRects["Sword"];
                Vector2 slotAPosition = new Vector2(445, 40);
                spriteBatch.Draw(itemsTexture, slotAPosition, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

            // Draw Slot B item sprite
            if (!string.IsNullOrEmpty(slotBItem) && itemSourceRects.ContainsKey(slotBItem))
            {
                Rectangle sourceRect = itemSourceRects[slotBItem];
                Vector2 slotBPosition = new Vector2(390, 40);
                spriteBatch.Draw(itemsTexture, slotBPosition, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            else
            {
                Rectangle sourceRect = itemSourceRects["Sword"];
                Vector2 slotBPosition = new Vector2(390, 40);
                spriteBatch.Draw(itemsTexture, slotBPosition, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        public void UpdateRoomNumber(int roomNumber)
        {
            currentRoomNumber = roomNumber;
        }
    }
}