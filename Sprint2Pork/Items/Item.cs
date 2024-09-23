using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Pork.Items
{
    public class Item : ISprite
    {
        private List<Rectangle> sourceRects;
        public Rectangle destinationRect;
        private int currentFrame;
        private int totalFrames;
        private int count;

        public Item(int x, int y, List<Rectangle> frames)
        {
            sourceRects = frames;
            currentFrame = 0;
            totalFrames = sourceRects.Count;
            destinationRect = new Rectangle(x, y, 16, 16); // Assuming each item is 16x16 pixels
            count = 0;
        }

        public void Update(int x, int y)
        {
            count++;
            if (count > 30)
            {
                currentFrame++;
                count = 0;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch sb, Texture2D txt)
        {
            sb.Draw(txt, destinationRect, sourceRects[currentFrame], Color.White);
        }
    }
}