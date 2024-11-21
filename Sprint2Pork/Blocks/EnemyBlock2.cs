using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class Block4 : Block
    {
        public Block4(Texture2D texture, Vector2 position, bool isMovable = false)
            : base(texture, position, new Rectangle(48, 0, 16, 17), isMovable)
        {
        }
    }
}
