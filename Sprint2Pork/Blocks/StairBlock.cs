using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class StairBlock : Block
    {
        public StairBlock(Texture2D texture, Vector2 position, bool isMovable = false)
            : base(texture, position, new Rectangle(112, 0, 16, 17), isMovable)
        {
        }
    }
}
