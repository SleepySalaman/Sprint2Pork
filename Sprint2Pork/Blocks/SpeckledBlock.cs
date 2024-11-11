using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class SpeckledBlock : Block
    {
        public SpeckledBlock(Texture2D texture, Vector2 position)
            : base(texture, position, new Rectangle(80, 0, 16, 17))
        {
        }
    }
}
