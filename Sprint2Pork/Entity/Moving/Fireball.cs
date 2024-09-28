using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public class Fireball : Entity {

        private int fireballID;

        private int changeX = 0;
        private int changeY = 0;

        private int relX;
        private int modX = 20;

        private int maxX = -50;

        public Fireball(int id, int x) {
            sourceRects = new List<Rectangle>() {
                new Rectangle(101, 14, 8, 10),
                new Rectangle(110, 14, 8, 10),
                new Rectangle(119, 14, 8, 10),
                new Rectangle(128, 14, 8, 10)
            };

            totalFrames = sourceRects.Count;
            fireballID = id;

            relX = x;

            destinationRect = new Rectangle(initX + relX + modX, initY, 20, 20);
        }

        public void Move() {
            changeX--;
            if(fireballID == 0) {
                changeY--;
            } else if(fireballID == 2) {
                changeY++;
            }
            destinationRect.X = initX + changeX + relX + modX;
            destinationRect.Y = initY + changeY;
        }
    }
}
