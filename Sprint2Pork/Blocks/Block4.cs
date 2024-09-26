using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Blocks
{
    public class Block4 : Block
    {
        public Block4(Texture2D texture, Vector2 position)
            : base(texture, position, new Rectangle(48, 0, 16, 16))
        {
        }
    }
}
