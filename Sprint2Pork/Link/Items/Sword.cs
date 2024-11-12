using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork
{
    public class Sword : ILinkItems
    {
        public int direction = 0;
        Rectangle rect = new Rectangle();
        int startX = 0;
        int startY = 0;
        public ISprite sprite;
        string directionStr = "Up";
        int offSetX = 0;
        int offSetY = 0;
        public Sword(ILinkDirectionState state, int X, int Y)
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
                    rect = new Rectangle(34, 0, 8, 16);
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Left";
                    startX += 70;
                    startY += 15;
                    rect = new Rectangle(34, 0, 8, 16);
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Up";
                    startX += 40;
                    startY += 70;
                    rect = new Rectangle(34, 0, 8, 16);
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    startY += -20;
                    rect = new Rectangle(34, 0, 8, 16);
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
            sprite = new MovingNonAnimatedSprite(startX + link.OffsetXGet(), startY + link.OffsetYGet(), rect, directionStr);
            link.UpdateItem();
        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sprite.Draw(sb, texture);
        }
        public Rectangle GetLocation() => (sprite.GetRect());
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
    }
}