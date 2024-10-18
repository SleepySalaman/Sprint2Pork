using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class Collision
    {

        public Collision()
        {

        }

        public bool Collides(Rectangle rect1, Rectangle rect2)
        {
            return (rect1.X + rect1.Width > rect2.X &&
                rect1.X < rect2.X + rect2.Width &&
                rect1.Y + rect1.Height > rect2.Y &&
                rect1.Y < rect2.Y + rect2.Height);
        }

        public bool CollidesWithGroundItem(Rectangle linkRect, Rectangle groundItemRect)
        {
            int groundItemCenterX = groundItemRect.X + groundItemRect.Width / 2;
            int groundItemCenterY = groundItemRect.Y + groundItemRect.Height / 2;

            return (linkRect.X < groundItemCenterX && linkRect.X + linkRect.Width > groundItemCenterX &&
                    linkRect.Y < groundItemCenterY && linkRect.Y + linkRect.Height > groundItemCenterY);
        }
    }
}
