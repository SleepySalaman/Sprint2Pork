using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using Sprint2Pork;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Minimap
{
    private const int MINIMAP_WIDTH = 5;
    private const int MINIMAP_HEIGHT = 5;
    private const int MINIMAP_X = 140;
    private const int MINIMAP_Y = 5;
    private const float SCALE_FACTOR = 0.20f;

    private Link link;
    private Texture2D minimapBackground;
    private Texture2D blockIndicator;
    private Texture2D itemIndicator;
    private Texture2D enemyIndicator;
    private Texture2D linkIndicator;

    public Minimap(GraphicsDevice graphicsDevice, Link link)
    {
        this.link = link;

        minimapBackground = new Texture2D(graphicsDevice, 1, 1);
        minimapBackground.SetData(new[] { Color.Black });

        blockIndicator = new Texture2D(graphicsDevice, 1, 1);
        blockIndicator.SetData(new[] { Color.Gray });

        itemIndicator = new Texture2D(graphicsDevice, 1, 1);
        itemIndicator.SetData(new[] { Color.Yellow });

        enemyIndicator = new Texture2D(graphicsDevice, 1, 1);
        enemyIndicator.SetData(new[] { Color.Red });

        linkIndicator = new Texture2D(graphicsDevice, 1, 1);
        linkIndicator.SetData(new[] { Color.Green });
    }

    public void Draw(SpriteBatch spriteBatch, List<Block> blocks, List<GroundItem> items, List<IEnemy> enemies)
    {
        // Draw background
        spriteBatch.Draw(minimapBackground, new Rectangle(MINIMAP_X, MINIMAP_Y, MINIMAP_WIDTH, MINIMAP_HEIGHT), Color.White * 0.7f);

        // Draw visible blocks
        foreach (Block block in blocks)
        {
            if (!(block is InvisibleBlock))  // Skip invisible blocks
            {
                Rectangle blockRect = block.getBoundingBox();
                float scaledX = MINIMAP_X + (blockRect.X * SCALE_FACTOR);
                float scaledY = MINIMAP_Y + (blockRect.Y * SCALE_FACTOR);
                float scaledWidth = blockRect.Width * SCALE_FACTOR;
                float scaledHeight = blockRect.Height * SCALE_FACTOR;

                spriteBatch.Draw(blockIndicator,
                    new Rectangle((int)scaledX, (int)scaledY, (int)scaledWidth, (int)scaledHeight),
                    Color.White);
            }
        }

        // Draw items
        foreach (var item in items)
        {
            Rectangle itemRect = item.GetRect();
            float scaledX = MINIMAP_X + (itemRect.X * SCALE_FACTOR);
            float scaledY = MINIMAP_Y + (itemRect.Y * SCALE_FACTOR);

            spriteBatch.Draw(itemIndicator,
                new Rectangle((int)scaledX, (int)scaledY, 3, 3),
                Color.White);
        }

        // Draw enemies
        foreach (var enemy in enemies)
        {
            Rectangle enemyRect = enemy.GetRect();
            float scaledX = MINIMAP_X + (enemyRect.X * SCALE_FACTOR);
            float scaledY = MINIMAP_Y + (enemyRect.Y * SCALE_FACTOR);

            spriteBatch.Draw(enemyIndicator,
                new Rectangle((int)scaledX, (int)scaledY, 3, 3),
                Color.White);
        }

        // Draw Link
        float linkScaledX = MINIMAP_X + (link.GetX() * SCALE_FACTOR);
        float linkScaledY = MINIMAP_Y + (link.GetY() * SCALE_FACTOR);
        spriteBatch.Draw(linkIndicator,
            new Rectangle((int)linkScaledX, (int)linkScaledY, 4, 4),
            Color.White);
    }
}
