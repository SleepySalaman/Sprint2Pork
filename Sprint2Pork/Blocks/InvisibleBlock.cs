using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class InvisibleBlock : Block
    {
        public InvisibleBlock(Texture2D texture, Vector2 position)
            : base(texture, position, new Rectangle(144, 17, 16, 17))
        {
        }
    }
}
