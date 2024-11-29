using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Windows.Forms.LinkLabel;

namespace Sprint2Pork
{
    public class Arrow : ILinkItems
    {
        private bool isBreaking = false;
        private int breakAnimationTimer = 0;
        private const int BREAK_ANIMATION_DURATION = 10;
        public int direction = 0;
        Rectangle rect = new Rectangle();
        string directionStr;
        int startX = 0;
        int startY = 0;
        public ISprite sprite;
        bool collided = false;
        public Arrow(ILinkDirectionState state, int X, int Y)
        {
            directionStr = "Down";
            switch (state)
            {
                case LeftFacingLinkState:
                    direction = 0;
                    directionStr = "Right";
                    startX = -35;
                    startY = 35;
                    rect = new Rectangle(27, 30, 7, 18); // 34 48
                    break;
                case RightFacingLinkState:
                    direction = 1;
                    directionStr = "Left";
                    startX = 80;
                    startY = 15;
                    rect = new Rectangle(27, 30, 7, 18); // 34 48
                    break;
                case DownFacingLinkState:
                    direction = 2;
                    directionStr = "Up";
                    startX = 35;
                    startY = 85;
                    rect = new Rectangle(27, 30, 7, 18); // 34 48
                    break;
                case UpFacingLinkState:
                    direction = 3;
                    directionStr = "Down";
                    startX = 10;
                    startY = -40;
                    rect = new Rectangle(27, 30, 7, 18); // 34 48
                    break;
            }
            startX += X;
            startY += Y;
            sprite = new MovingNonAnimatedSprite(startX, startY, rect, directionStr);
        }

        public void Update(Link link)
        {
            //if (isBreaking)
            //{
            //    breakAnimationTimer++;
            //    rect = new Rectangle(51, 34, 10, 9); // Where is this rectangle on the screen?
            //    sprite = new MovingNonAnimatedSprite(startX + link.OffsetXGet(), startY + link.OffsetYGet(), rect, directionStr);
            //    if(breakAnimationTimer >= BREAK_ANIMATION_DURATION)
            //    {
            //        link.LoseItem();
            //        //isBreaking = false;
            //        //collided = false;
            //    }
            //    return;
            //}
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
            sprite = new MovingNonAnimatedSprite(startX + link.OffsetXGet(), startY + link.OffsetYGet(), rect, directionStr);

            //Explosion
            if (link.LinkCountGet() >= 19)
            {
                rect = new Rectangle(51, 34, 10, 9); // 170 47
                if (link.directionState is UpFacingLinkState)
                {
                    link.OffsetXChange(5);
                    link.OffsetYChange(-25);
                }
                else if (link.directionState is DownFacingLinkState)
                {
                    link.OffsetXChange(-10);
                    link.OffsetYChange(30);
                }
                sprite = new MovingNonAnimatedSprite(startX + link.OffsetXGet(), startY + link.OffsetYGet(), rect, directionStr);
            }

            link.UpdateItem();
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sprite.Draw(sb, texture);
        }
        public bool Collides(Rectangle rect2)
        {
            if (collided || isBreaking) return false;

            Rectangle rect1 = sprite.GetRect();
            if (rect1.X + rect1.Width > rect2.X &&
                rect1.X < rect2.X + rect2.Width &&
                rect1.Y + rect1.Height > rect2.Y &&
                rect1.Y < rect2.Y + rect2.Height)
            {
                collided = true;
                //isBreaking = true;
                //breakAnimationTimer = 0;
                rect = new Rectangle(0, 69, 7, 18);
                sprite = new MovingNonAnimatedSprite(startX, startY, rect, directionStr);
                return true;
            }

            return false;
        }
        public Rectangle getLocation() => (sprite.GetRect());
        public void SpriteSet(ISprite sprite) => this.sprite = sprite;
        public ISprite SpriteGet() => sprite;
        public string GetItemName() => "Rupee";
    }

}