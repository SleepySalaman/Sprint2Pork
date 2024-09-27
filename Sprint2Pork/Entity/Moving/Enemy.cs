using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public abstract class Enemy : IEnemy {

        protected int totalFrames;
        protected int currentFrame = 0;

        protected int count = 0;
        protected int maxCount = 30;

        protected int initX = 450;
        protected int initY = 350;

        protected int rectW = 100;
        protected int rectH = 100;

        protected List<Rectangle> sourceRects;
        protected Rectangle destinationRect;

        public void Update() {
            count++;
            if (count > maxCount) {
                currentFrame++;
                count = 0;
                if (currentFrame == totalFrames) {
                    currentFrame = 0;
                }
            }
        }

        public abstract void Move();

        public abstract void Attack();

        public void Draw(SpriteBatch sb, Texture2D txt) {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], Color.White);
        }

    }
}
