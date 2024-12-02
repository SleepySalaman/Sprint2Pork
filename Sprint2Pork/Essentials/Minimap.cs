using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using System.Collections.Generic;

public class Minimap
{
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

    public void Draw(SpriteBatch spriteBatch, List<Block> blocks, List<GroundItem> items, List<IEnemy> enemies, Rectangle bounds, float scale)
    {
        spriteBatch.Draw(minimapBackground, bounds, Color.White * 0.15f);

        foreach (Block block in blocks)
        {
            if (!(block is InvisibleBlock))
            {
                Rectangle blockRect = block.GetBoundingBox();
                float scaledX = bounds.X + (blockRect.X * scale);
                float scaledY = bounds.Y + (blockRect.Y * scale);
                float scaledWidth = blockRect.Width * scale;
                float scaledHeight = blockRect.Height * scale;

                spriteBatch.Draw(blockIndicator,
                    new Rectangle((int)scaledX, (int)scaledY, (int)scaledWidth, (int)scaledHeight),
                    Color.White);
            }
        }

        int itemSize = bounds.Width <= 100 ? 2 : 6;
        foreach (var item in items)
        {
            Rectangle itemRect = item.GetRect();
            float scaledX = bounds.X + (itemRect.X * scale);
            float scaledY = bounds.Y + (itemRect.Y * scale);

            spriteBatch.Draw(itemIndicator,
                new Rectangle((int)scaledX, (int)scaledY, itemSize, itemSize),
                Color.White);
        }

        foreach (var enemy in enemies)
        {
            Rectangle enemyRect = enemy.GetRect();
            float scaledX = bounds.X + (enemyRect.X * scale);
            float scaledY = bounds.Y + (enemyRect.Y * scale);

            spriteBatch.Draw(enemyIndicator,
                new Rectangle((int)scaledX, (int)scaledY, 6, 6),
                Color.White);
        }

        float linkScaledX = bounds.X + (link.GetX() * scale);
        float linkScaledY = bounds.Y + (link.GetY() * scale);
        spriteBatch.Draw(linkIndicator,
            new Rectangle((int)linkScaledX, (int)linkScaledY, 8, 8),
            Color.White);
    }
}

