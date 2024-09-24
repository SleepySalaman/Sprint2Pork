using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Enemies.Gohma {
    public class GohmaAttacking : IEnemy {

        private int totalFrames;
        private int currentFrame;

        private List<Rectangle> sourceRects;
        private Rectangle destinationRect;

        private int count;
        private int maxCount;

        private int moveCount;
        private int moveMaxCount;

        private int initX;
        private int initY;

        private int relativeX;
        private int relativeY;

        private bool movingRight;

        private int scaleX;
        private int scaleY;

        public GohmaAttacking() {
            sourceRects = new List<Rectangle> {
                new Rectangle(220, 0, 40, 40),
                new Rectangle(260, 0, 40, 40)
            };

            scaleX = 1;
            scaleY = 1;

            initX = 450;
            initY = 350;

            relativeX = 0;
            relativeY = 0;

            movingRight = true;

            count = 0;
            maxCount = 30;

            moveCount = 0;
            moveMaxCount = 2;

            currentFrame = 0;
            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, 100 * scaleX, 100 * scaleY);
        }

        public void Update() {
            count++;
            if (count > maxCount) {
                currentFrame++;
                count = 0;
                if (currentFrame == totalFrames) {
                    currentFrame = 0;
                }
            }

            destinationRect.X = initX + relativeX;
        }

        public void takeDamage() {

        }

        public void Draw(SpriteBatch sb, Texture2D txt) {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], Color.White);
        }

    }
}
