using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class Paused
    {
        private Inventory inventory;
        private Dictionary<string, Rectangle> itemSourceRects;

        public Paused(Inventory gameInventory)
        {
            inventory = gameInventory ?? new Inventory();
            InitializeItemSourceRects();
        }

        private void InitializeItemSourceRects()
        {
            itemSourceRects = new Dictionary<string, Rectangle>
            {
                { "Rupee", new Rectangle(72, 0, 8, 16) },
                { "Key", new Rectangle(240, 0, 8, 16) },
                { "GroundBomb", new Rectangle(136, 0, 8, 16) }
            };
        }

        public void DrawPausedScreen(SpriteBatch spriteBatch, SpriteFont font, Viewport viewport, Texture2D itemsTexture)
        {
            int yOffset = 200;
            int titleSpacing = 40;
            int itemSpacing = 80;
            float scale = 3.0f;

            string title = "GAME PAUSED";
            Vector2 titlePos = new Vector2((viewport.Width - font.MeasureString(title).X) / 2, yOffset);
            spriteBatch.DrawString(font, title, titlePos, Color.White);

            yOffset += titleSpacing;

            string[] itemTypes = { "Rupee", "Key", "GroundBomb" };
            foreach (var itemType in itemTypes)
            {
                Rectangle sourceRect = itemSourceRects[itemType];
                Vector2 itemPos = new Vector2(viewport.Width / 2 - (sourceRect.Width * scale) / 2, yOffset);
                spriteBatch.Draw(itemsTexture, itemPos, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                string countText = $"x{inventory.GetItemCount(itemType)}";
                Vector2 countPos = new Vector2(itemPos.X + (sourceRect.Width * scale) + 10, yOffset);
                spriteBatch.DrawString(font, countText, countPos, Color.White);

                yOffset += itemSpacing;
            }
        }
    }
}
