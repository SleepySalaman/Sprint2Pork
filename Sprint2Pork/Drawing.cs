using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Constants;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using Sprint2Pork.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Sprint2Pork
{
    public class Drawing
    {
        public static void DrawGeneratedObjects(SpriteBatch spriteBatch, List<Block> blocks, List<GroundItem> groundItems,
            List<IEnemy> enemies, List<EnemyManager> fireballManagers, List<Texture2D> allTextures, Texture2D lifeTexture,
            Texture2D hitboxTexture, bool showHitbox)
        {
            foreach (Block block in blocks)
            {
                block.Draw(spriteBatch);
            }

            foreach (var item in groundItems)
            {
                item.Draw(spriteBatch, allTextures[9]);
            }

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, allTextures[enemy.getTextureIndex()], lifeTexture, hitboxTexture, showHitbox);
            }
            foreach (var fireball in fireballManagers)
            {
                fireball.Draw(spriteBatch, allTextures[1], hitboxTexture, showHitbox);
            }
        }

        public static void DrawGameState(Game1State gameState, SpriteBatch spriteBatch, SpriteFont font, Texture2D winStateTexture, Viewport viewport, Game1 game)
        {
            switch (gameState)
            {
                case Game1State.Playing:
                    game.DrawPlayingScreen();
                    break;
                case Game1State.Transitioning:
                    game.DrawTransitioningScreen();
                    break;
                case Game1State.Paused:
                case Game1State.Inventory:
                    game.DrawInventoryScreen();
                    break;
                case Game1State.GameOver:
                    spriteBatch.DrawString(font, "Game Over", new Vector2(GameConstants.TEXT_DISPLAY, GameConstants.TEXT_DISPLAY), Color.Red);
                    break;
            }
        }

        public static void DrawGameOverScreen(SpriteBatch spriteBatch, Texture2D hitboxTexture, SpriteFont font, Viewport viewport)
        {
            int margin = 50;
            spriteBatch.Draw(hitboxTexture,
                new Rectangle(margin,
                             GameConstants.HUD_HEIGHT + margin,
                             viewport.Width - (margin * 2),
                             viewport.Height - GameConstants.HUD_HEIGHT - (margin * 2)),
                Color.Black);

            Vector2 textPosition = new Vector2(viewport.Width / 2 - font.MeasureString("GAME OVER").X / 2,
                                             viewport.Height / 2 - font.MeasureString("GAME OVER").Y / 2);
            spriteBatch.DrawString(font, "Game Over\n\nPress R to Restart", textPosition, Color.Red);
        }

        public static void DrawInventoryScreen(SpriteBatch spriteBatch, HUD hud, Minimap minimap, List<Block> blocks, List<GroundItem> groundItems, List<IEnemy> enemies, SpriteFont font, Viewport viewport, Texture2D itemsTexture, Game1 game)
        {
            hud.Draw(spriteBatch, itemsTexture);
            minimap.Draw(spriteBatch, blocks, groundItems, enemies, new Rectangle(120, 15, 140, 5), 0.15f);

            string title = "GAME PAUSED";
            Vector2 titlePos = new Vector2((viewport.Width - font.MeasureString(title).X) / 2, GameConstants.INVENTORY_PAUSED_TEXT_HEIGHT);
            spriteBatch.DrawString(font, title, titlePos, Color.White);
            DrawBSlotInventoryContents(spriteBatch, game, itemsTexture, game.allTextures[9], game.link, minimap, blocks, groundItems, enemies);
        }

        private static void InitializeBSlotItemSourceRects(out Dictionary<string, Rectangle> itemSourceRects)
        {
            itemSourceRects = new Dictionary<string, Rectangle>
            {
                { "GroundBomb", new Rectangle(136, 0, 8, 16) },
                { "Sword", new Rectangle(104, 16, 6, 16) },
                { "Arrow", new Rectangle(152, 16, 8, 16) },
                { "Boomerang", new Rectangle(128, 0, 8, 16) },
                { "WoodArrow", new Rectangle(152, 0, 8, 16) },
                { "BlueBoomer", new Rectangle(128, 16, 8, 16) },
                { "Fire", new Rectangle(224, 0, 8, 16) },
            };
        }

        public static void DrawBSlotInventoryContents(SpriteBatch spriteBatch, Game1 game, Texture2D itemsTexture, Texture2D hitboxTexture, Link link, Minimap minimap, List<Block> blocks, List<GroundItem> groundItems, List<IEnemy> enemies, float scale = 4.0f)
        {
            int startX = GameConstants.INVENTORY_START_X;
            int startY = GameConstants.INVENTORY_START_Y;
            int itemSize = GameConstants.INVENTORY_ITEM_SIZE;
            int padding = GameConstants.INVENTORY_PADDING;
            int itemsPerRow = GameConstants.INVENTORY_ITEMS_PER_ROW;
            InitializeBSlotItemSourceRects(out var itemSourceRects);

            int boxWidth = (itemSize + padding) * itemsPerRow - padding;
            int boxHeight = (itemSize + padding) * (itemSourceRects.Count / itemsPerRow + 1) - padding;

            for (int i = 0; i < itemSourceRects.Count; i++)
            {
                var item = itemSourceRects.ElementAt(i);
                Rectangle sourceRect = item.Value;
                int row = i / itemsPerRow;
                int col = i % itemsPerRow;
                Vector2 position = new Vector2(startX + (itemSize + padding) * col, startY + (itemSize + padding) * row);

                spriteBatch.Draw(itemsTexture, position, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                if (item.Key == link.SlotB)
                {
                    DrawSelectionBox(spriteBatch, new Rectangle((int)position.X, (int)position.Y, (int)(sourceRect.Width * scale), (int)(sourceRect.Height * scale)));
                }
            }
            minimap.Draw(spriteBatch, blocks, groundItems, enemies, new Rectangle(20, 140, 200, 200), 0.5f);
            Rectangle minimapRectangle = new Rectangle(60, 210, 320, 158);
            DrawBlueBox(spriteBatch, minimapRectangle);
            DrawBlueBox(spriteBatch, new Rectangle(startX - padding, startY - padding, boxWidth + 2 * padding, boxHeight + 2 * padding));
        }

        public static void DrawSelectionBox(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            int thickness = GameConstants.INVENTORY_LINE_THICKNESS;
            int length = GameConstants.INVENTORY_SELECTION_BOX_LENGTH_ADJUSTMENT;
            Color color = Color.Red;

            rectangle = new Rectangle(rectangle.Left - 3, rectangle.Top - 3, rectangle.Width + 6, rectangle.Height + 6);
            Texture2D texture = CreateSolidColorTexture(spriteBatch.GraphicsDevice, color);

            spriteBatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Top, length, thickness), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Top, thickness, length), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Right - length, rectangle.Top, length, thickness), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, length), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, length, thickness), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Bottom - length, thickness, length), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Right - length, rectangle.Bottom - thickness, length, thickness), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Right - thickness, rectangle.Bottom - length, thickness, length), color);
        }

        public static void DrawBlueBox(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            int thickness = GameConstants.INVENTORY_LINE_THICKNESS;
            Color color = Color.Blue;
            Texture2D blueBoxTexture = CreateSolidColorTexture(spriteBatch.GraphicsDevice, color);

            spriteBatch.Draw(blueBoxTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
            spriteBatch.Draw(blueBoxTexture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width, thickness), color);
            spriteBatch.Draw(blueBoxTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
            spriteBatch.Draw(blueBoxTexture, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, rectangle.Height), color);
        }

        public static Texture2D CreateSolidColorTexture(GraphicsDevice graphicsDevice, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }

    }
}
