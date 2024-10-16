using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Entity.Moving
{
    public class Digdogger : Enemy
    {

        private List<Rectangle> digdoggerLarge;
        private List<Rectangle> digdoggerSmall;

        private int digdoggerSelector = 0;
        private bool switchMode = false;
        private int switchTimer = 0;
        private int maxSwitchTime = 150;

        public Digdogger(int initX, int initY){
            sourceRects = new List<Rectangle>();

            digdoggerLarge = new List<Rectangle> {
                new Rectangle(220, 0, 40, 40),
                new Rectangle(260, 0, 40, 40)
            };

            digdoggerSmall = new List<Rectangle> {
                new Rectangle(196, 8, 20, 20),
                new Rectangle(176, 8, 20, 20)
            };

            totalFrames = sourceRects.Count;

            destinationRect = new Rectangle(initX, initY, rectW, rectH);
        }

        public override void Move()
        {
            if (!switchMode)
            {
                switchMode = true;
                digdoggerSelector = new Random().Next(1, 3);
                if (digdoggerSelector == 1)
                {
                    sourceRects = digdoggerLarge;
                    destinationRect.Width = rectW;
                    destinationRect.Height = rectH;
                }
                else
                {
                    sourceRects = digdoggerSmall;
                    destinationRect.Width = rectW / 2;
                    destinationRect.Height = rectH / 2;
                }
                totalFrames = sourceRects.Count;
            }
            else
            {
                switchTimer++;
                if (switchTimer > maxSwitchTime)
                {
                    switchMode = false;
                    switchTimer = 0;
                }
            }
        }
    }
}
