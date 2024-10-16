using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class StripedBlock : Block
    {
        public StripedBlock(Texture2D texture, Vector2 position)
            : base(texture, position, new Rectangle(144, 0, 16, 16))
        {
        }
    }
}
