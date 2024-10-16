using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class DarkBlueBlock : Block
    {
        public DarkBlueBlock(Texture2D texture, Vector2 position)
            : base(texture, position, new Rectangle(96, 0, 16, 16))
        {
        }
    }
}
