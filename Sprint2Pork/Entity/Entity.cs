using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sprint2Pork.Entity {
    public abstract class Entity : IEntity {

        protected int totalFrames;
        protected int currentFrame;

        protected List<Rectangle> sourceRects;
        protected Rectangle destinationRect;

        protected int count;
        protected int maxCount;

        protected int initX;
        protected int initY;

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

        public void Draw(SpriteBatch sb, Texture2D txt) {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], Color.White);
        }

    }
}
