using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    public class EnemyBlock1 : Block
    {
        public EnemyBlock1(Texture2D texture, Vector2 position)
            : base(texture, position, new Rectangle(32, 0, 16, 16))
        {
        }
    }
}
