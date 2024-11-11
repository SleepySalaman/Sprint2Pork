using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Items
{
    public class GroundBomb : GroundItem
    {
        public GroundBomb(int x, int y, List<Rectangle> frames) : base(x, y, frames)
        {
        }

        public override void PerformAction()
        {
            Console.WriteLine("Bomb collected!");
        }
    }
}