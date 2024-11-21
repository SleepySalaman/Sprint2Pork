using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class StripedBlock : Block
    {
        public StripedBlock(Texture2D texture, Vector2 position, bool isMovable = false)
            : base(texture, position, new Rectangle(144, 0, 16, 17), isMovable)
        {
        }
    }
}
