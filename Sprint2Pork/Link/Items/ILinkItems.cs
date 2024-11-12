using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    public interface ILinkItems
    {
        void Update(Link link);
        public void Draw(SpriteBatch sb, Texture2D texture);

        public (int X, int Y) getLocation(Link link);
        public void SpriteSet(ISprite sprite);
        public ISprite SpriteGet();
    }
}
