using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class Block
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle SourceRect { get; set; } // Rectangle to define which part of the sprite sheet is used

        public Block(Texture2D texture, Vector2 position, Rectangle sourceRect)
        {
            Texture = texture;
            Position = position;
            SourceRect = sourceRect;
        }

        public void Draw(SpriteBatch spriteBatch, float scale = 2f)
        {
            // Use the scaling factor in the Draw call
            spriteBatch.Draw(Texture, Position, SourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
