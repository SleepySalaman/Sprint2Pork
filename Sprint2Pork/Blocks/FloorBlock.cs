using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class FloorBlock : Block
    {
        public FloorBlock(Texture2D texture, Vector2 position, bool isMovable = false)
            : base(texture, position, new Rectangle(0, 0, 16, 16), isMovable)
        {
        }
    }

}
