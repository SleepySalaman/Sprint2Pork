using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Fireball : Entity
    {
        private int startX, startY;

        private int fireballID;

        private int changeX = 0;
        private int changeY = 0;

        private int relX;
        private int modX = 20;

        private int maxX = -50;

        public Fireball(int id, int x, int initX, int initY){
            sourceRects = new List<Rectangle>() {
                new Rectangle(101, 14, 8, 10),
                new Rectangle(110, 14, 8, 10),
                new Rectangle(119, 14, 8, 10),
                new Rectangle(128, 14, 8, 10)
            };

            startX = initX;
            startY = initY;

            totalFrames = sourceRects.Count;
            fireballID = id;

            relX = x;

            destinationRect = new Rectangle(initX + relX + modX, initY, 20, 20);
        }

        public Rectangle GetRect()
        {
            return new Rectangle(destinationRect.X, destinationRect.Y, sourceRects[0].Width, sourceRects[0].Height);
        }

        public void Move()
        {
            changeX--;
            if (fireballID == 0)
            {
                changeY--;
            }
            else if (fireballID == 2)
            {
                changeY++;
            }
            destinationRect.X = startX + changeX + relX + modX;
            destinationRect.Y = startY + changeY;
        }
    }
}
