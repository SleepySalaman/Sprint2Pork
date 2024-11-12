using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public interface ILinkItems
    {
        void Update(Link link);
        public void Draw(SpriteBatch sb, Texture2D texture);

        public bool Collides(Rectangle rect2);
        public Rectangle getLocation();
        public void SpriteSet(ISprite sprite);
        public ISprite SpriteGet();
        string GetItemName();
    }
}
