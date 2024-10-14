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
        protected int currentFrame = 0;

        protected int count = 0;
        protected int maxCount = 30;

        protected int initX = 450;
        protected int initY = 350;

        protected int rectW = 100;
        protected int rectH = 100;

        protected List<Rectangle> sourceRects;
        protected Rectangle destinationRect;

        protected Color c = Color.White;

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
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], c);
        }

        public Rectangle getHitbox() {
            return destinationRect;
        }

        public void udpateFromCollision(bool collides) {
            if (collides) {
                c = Color.Red;
            } else {
                c = Color.White;
            }
        }
    }
}
