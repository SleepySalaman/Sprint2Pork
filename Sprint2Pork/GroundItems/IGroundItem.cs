using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.GroundItems
{
    public interface IGroundItem
    {
        void Update(int x, int y);
        void Draw(SpriteBatch spriteBatch, Texture2D texture);
        void PerformAction();
    }
}
