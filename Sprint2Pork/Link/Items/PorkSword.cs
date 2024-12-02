using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    public class PorkSword : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        int startX = 0;
        int startY = 0;
        public ISprite sprite;
        string directionStr = "Up";
        bool collided = false;
        public PorkSword(ILinkDirectionState state, int X, int Y)
        {
            startX += X;
            startY += Y;

            switch (state)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Right";
                    startX += -10;
                    startY += 45;
                    rect = new Rectangle(104, 0, 8, 16);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Left";
                    startX += 70;
                    startY += 15;
                    rect = new Rectangle(104, 0, 8, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Up";
                    startX += 40;
                    startY += 70;
                    rect = new Rectangle(104, 0, 8, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    startY += -20;
                    rect = new Rectangle(104, 0, 8, 16);
                    break;
            }
            sprite = new MovingNonAnimatedSprite(startX, startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.OffsetXChange((link.LinkCountGet() <= 10) ? -7 : 7);
            }
            else if (direction == 1)
            {
                link.OffsetXChange((link.LinkCountGet() <= 10) ? 7 : -7);
            }
            else if (direction == 2)
            {
                link.OffsetYChange((link.LinkCountGet() <= 10) ? 7 : -7);
            }
            else if (direction == 3)
            {
                link.OffsetYChange((link.LinkCountGet() <= 10) ? -7 : 7);
            }
            sprite.Update(startX + link.OffsetXGet(), startY + link.OffsetYGet());
            link.UpdateItem();
        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sprite.Draw(sb, texture);
        }
        public bool Collides(Rectangle rect2)
        {
            if (collided)
            {
                return false;
            }
            Rectangle rect1 = sprite.GetRect();
            if (rect1.X + rect1.Width > rect2.X &&
                rect1.X < rect2.X + rect2.Width &&
                rect1.Y + rect1.Height > rect2.Y &&
                rect1.Y < rect2.Y + rect2.Height)
            {
                collided = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public Rectangle getLocation() => (sprite.GetRect());
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
        public string GetItemName() => "Sword";

    }
}