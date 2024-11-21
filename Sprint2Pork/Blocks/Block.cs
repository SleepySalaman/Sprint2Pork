using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class Block
    {
        public bool IsMovable { get; set; }

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle SourceRect { get; set; }
        private float scale = 1.875f;
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, (int)(SourceRect.Width * scale), (int)(SourceRect.Height * scale));
        public const int TileSize = 43; // Assuming 16px * 3 scale

        public Block(Texture2D texture, Vector2 position, Rectangle sourceRect, bool isMovable = false)
        {
            Texture = texture;
            Position = position;
            SourceRect = sourceRect;
            IsMovable = isMovable;
        }
        public void Move(ILinkDirectionState direction)
        {
            if (!IsMovable) return;
            
            if (direction is LeftFacingLinkState)
                Position = new Vector2(Position.X - TileSize, Position.Y);
            else if (direction is RightFacingLinkState)
                Position = new Vector2(Position.X + TileSize, Position.Y);
            else if (direction is UpFacingLinkState)
                Position = new Vector2(Position.X, Position.Y - TileSize);
            else if (direction is DownFacingLinkState)
                Position = new Vector2(Position.X, Position.Y + TileSize);
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
