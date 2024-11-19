using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    public class Fire : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        int startX = 0;
        int startY = 0;
        string directionStr;
        public ISprite sprite;
        bool collided = false;

        public Fire(ILinkDirectionState state, int X, int Y)
        {

            directionStr = "Down";
            switch (state)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    startX = -50;
                    startY = 0;
                    rect = new Rectangle(188, 30, 19, 18); // 207 48
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    startX = 30;
                    startY = -0;
                    rect = new Rectangle(188, 30, 19, 18);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    startX = 0;
                    startY = 20;
                    rect = new Rectangle(188, 30, 19, 18);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    startX = -10;
                    startY = -40;
                    rect = new Rectangle(188, 30, 19, 18);
                    break;
            }
            startX += X;
            startY += Y;
            sprite = new MovingNonAnimatedSprite(startX, startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            if (direction == 0)
            {
                link.OffsetXChange(-12);
            }
            else if (direction == 1)
            {
                link.OffsetXChange(12);
            }
            else if (direction == 2)
            {
                link.OffsetYChange(12);
            }
            else if (direction == 3)
            {
                link.OffsetYChange(-12);
            }
            link.UpdateItem();
            sprite.Update(link.OffsetXGet() + startX, link.OffsetYGet() + startY);
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
        public string GetItemName() => "Fire";

    }
}
