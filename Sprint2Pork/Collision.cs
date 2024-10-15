using Microsoft.Xna.Framework;

namespace Sprint2Pork
{
    public class Collision
    {

        public Collision()
        {

        }

        public bool collides(Rectangle rect1, Rectangle rect2)
        {
            return (rect1.X + rect1.Width > rect2.X &&
                rect1.X < rect2.X + rect2.Width &&
                rect1.Y + rect1.Height > rect2.Y &&
                rect1.Y < rect2.Y + rect2.Height);
        }

        public int collisionDirection()
        {
            return 0;
        }

    }
}
