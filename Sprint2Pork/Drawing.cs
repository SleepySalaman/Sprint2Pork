using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Constants;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using System.Collections.Generic;

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

        public static void DrawInventoryScreen(SpriteBatch spriteBatch, HUD hud, Minimap minimap, List<Block> blocks, List<GroundItem> groundItems, List<IEnemy> enemies, SpriteFont font, Viewport viewport, Texture2D itemsTexture, Game1 game)
        {
            hud.Draw(spriteBatch, itemsTexture);
            minimap.Draw(spriteBatch, blocks, groundItems, enemies, new Rectangle(120, 15, 140, 5), 0.15f);

            string title = "GAME PAUSED";
            Vector2 titlePos = new Vector2((viewport.Width - font.MeasureString(title).X) / 2, 150);
            spriteBatch.DrawString(font, title, titlePos, Color.White);
            game.DrawInventoryItems();
        }

        public static void DrawSelectionBox(SpriteBatch spriteBatch, Texture2D hitboxTexture, Rectangle rectangle)
        {
            int thickness = 3;
            int length = 9;
            Color color = Color.Red;

            rectangle = new Rectangle(rectangle.Left - 3, rectangle.Top - 3, rectangle.Width + 6, rectangle.Height + 4);

            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Left, rectangle.Top, length, thickness), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, length), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Right - length, rectangle.Top, length, thickness), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, length), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, length, thickness), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Left, rectangle.Bottom - length, thickness, length), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Right - length, rectangle.Bottom - thickness, length, thickness), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Right - thickness, rectangle.Bottom - length, thickness, length), color);
        }

        public static void DrawBlueBox(SpriteBatch spriteBatch, Texture2D texture, Rectangle rectangle)
        {
            int thickness = 3;
            Color color = Color.Blue;

            spriteBatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width, thickness), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, rectangle.Height), color);
        }
    }
}
