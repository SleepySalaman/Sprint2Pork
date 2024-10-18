using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class Block
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle SourceRect { get; set; }
        private float scale = 1.875f;
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, (int)(SourceRect.Width * scale), (int)(SourceRect.Height * scale));


        public Block(Texture2D texture, Vector2 position, Rectangle sourceRect)
        {
            Texture = texture;
            Position = position;
            SourceRect = sourceRect;
        }

        public void Draw(SpriteBatch spriteBatch, float scale = 2.4f)
        {
            // Use the scaling factor in the Draw call
            spriteBatch.Draw(Texture, Position, SourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
        public Rectangle getBoundingBox()
        {
            return BoundingBox;
        }
    }
}
