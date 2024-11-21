using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class Block2 : Block
    {
        public Block2(Texture2D texture, Vector2 position, bool isMovable = false)
            : base(texture, position, new Rectangle(16, 0, 16, 16), isMovable)
        {
        }
    }
}
