using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class Block3 : Block
    {
        public Block3(Texture2D texture, Vector2 position)
            : base(texture, position, new Rectangle(32, 0, 16, 16))
        {
        }
    }
}
