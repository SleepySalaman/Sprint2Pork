using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Blocks
{
    internal interface IBlock
    {
        void Update(int x, int y);
        void Draw(SpriteBatch spriteBatch, Texture2D texture);
        void PerformAction();
    }
}
