using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    internal class NoItem : ILinkItems
    {
        private ISprite sprite;

        public void Update(Link link)
        {
            link.loseItem();
        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sprite.Draw(sb, texture);
        }
    }
}
