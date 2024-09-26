using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Blocks
{
    internal interface IBlock
    {
        void Update(int x, int y);
        void Draw(SpriteBatch spriteBatch, Texture2D texture);
        void PerformAction();
    }
}
