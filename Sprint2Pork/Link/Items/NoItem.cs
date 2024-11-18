using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    internal class NoItem : ILinkItems
    {
        public ISprite sprite;

        public void Update(Link link)
        {
            link.loseItem();
        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            return;
        }

        public bool Collides(Rectangle rect2)
        {
            Rectangle rect1 = new Rectangle(0, 0, 0, 0);
            return (rect1.X + rect1.Width > rect2.X &&
                rect1.X < rect2.X + rect2.Width &&
                rect1.Y + rect1.Height > rect2.Y &&
                rect1.Y < rect2.Y + rect2.Height);
        }
        public Rectangle getLocation() => (new Rectangle(0, 0, 0, 0));
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
        public string GetItemName() => "NoItem";

    }
}
