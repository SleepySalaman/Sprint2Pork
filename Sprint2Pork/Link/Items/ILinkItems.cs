using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    public interface ILinkItems
    {
        void Update(Link link);
        public void Draw(SpriteBatch sb, Texture2D texture);
    }
}
