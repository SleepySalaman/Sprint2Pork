using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class LinkAttackingSpriteFactory
    {
        private List<Rectangle> leftRects;
        private List<Rectangle> rightRects;
        private List<Rectangle> upRects;
        private List<Rectangle> downRects;
        public LinkAttackingSpriteFactory()
        {
            leftRects = new List<Rectangle>();
            // no sword
            leftRects.Add(new Rectangle(0, 43, 16, 16));
            // half extended brown
            leftRects.Add(new Rectangle(69, 43, 22, 16));
            // fully extended brown
            leftRects.Add(new Rectangle(45, 43, 23, 16));
            // fully extended blue
            leftRects.Add(new Rectangle(16, 43, 28, 16));

            // fully extended brown
            leftRects.Add(new Rectangle(45, 43, 23, 16));
            // half extended brown
            leftRects.Add(new Rectangle(69, 43, 22, 16));
            // no sword
            leftRects.Add(new Rectangle(0, 43, 16, 16));

            rightRects = new List<Rectangle>();
            // no sword
            rightRects.Add(new Rectangle(0, 43, 16, 16));
            // half extended brown
            rightRects.Add(new Rectangle(69, 43, 22, 16));
            // fully extended brown
            rightRects.Add(new Rectangle(45, 43, 23, 16));
            // fully extended blue
            rightRects.Add(new Rectangle(16, 43, 28, 16));

            // fully extended brown
            rightRects.Add(new Rectangle(45, 43, 23, 16));
            // half extended brown
            rightRects.Add(new Rectangle(69, 43, 22, 16));
            // no sword
            rightRects.Add(new Rectangle(0, 43, 16, 16));

            upRects = new List<Rectangle>();
            // no sword
            upRects.Add(new Rectangle(0, 86, 16, 17));
            // short sword
            upRects.Add(new Rectangle(50, 82, 16, 22));
            // 3/4 sword blue
            upRects.Add(new Rectangle(34, 75, 16, 29));
            // full sword blue
            upRects.Add(new Rectangle(17, 75, 16, 29));
            // 3/4 sword blue
            upRects.Add(new Rectangle(34, 75, 16, 29));
            // short sword
            upRects.Add(new Rectangle(50, 82, 16, 22));
            // no sword
            upRects.Add(new Rectangle(0, 86, 16, 17));

            downRects = new List<Rectangle>();
            // no sword
            downRects.Add(new Rectangle(0, 16, 16, 16));
            //short sword
            downRects.Add(new Rectangle(50, 16, 16, 22));
            // half sword
            downRects.Add(new Rectangle(33, 16, 16, 26));
            // full sword blue
            downRects.Add(new Rectangle(16, 16, 16, 27));
            // half sword
            downRects.Add(new Rectangle(33, 16, 16, 26));
            //short sword
            downRects.Add(new Rectangle(50, 16, 16, 22));
            // no sword
            downRects.Add(new Rectangle(0, 16, 16, 16));
        }

        public void SetLinkAttackSprite(Link link) { 
            switch (link.directionState)
            {
                // TODO: Update rects to be correct attack animations for each direction
                case LeftFacingLinkState:
                    link.LinkSpriteSet(new MovingAnimatedSprite(link.GetX(), link.GetY(), leftRects, true, 5, "Left")); // non-moving?
                    break;
                case RightFacingLinkState:
                    
                    link.LinkSpriteSet(new MovingAnimatedSprite(link.GetX(), link.GetY(), rightRects, false, 5, "Right")); // non-moving?
                    break;
                case UpFacingLinkState:
                    
                    link.LinkSpriteSet(new MovingAnimatedSprite(link.GetX(), link.GetY(), upRects, false, 5, "Up")); // non-moving?
                    break;
                case DownFacingLinkState:
                    
                    link.LinkSpriteSet(new MovingAnimatedSprite(link.GetX(), link.GetY(), downRects, false, 5, "Down")); // non-moving?
                    break;
            }
        }

    }
    }

