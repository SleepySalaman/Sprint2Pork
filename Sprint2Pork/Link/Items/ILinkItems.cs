using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public interface ILinkItems
    {
        void Update(Link link);
        public void Draw(SpriteBatch sb, Texture2D texture);

        public Rectangle getLocation();
        public void SpriteSet(ISprite sprite);
        public ISprite SpriteGet();
    }
}
