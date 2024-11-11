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
            sprite = new MovingNonAnimatedSprite(link.OffsetXGet() + startX, link.OffsetYGet() + startY, rect, directionStr);
        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sprite.Draw(sb, texture);
        }
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
    }
}
