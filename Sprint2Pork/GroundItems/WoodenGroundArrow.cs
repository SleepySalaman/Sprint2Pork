using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Items
{
    public class WoodenGroundArrow : GroundItem
    {
        public WoodenGroundArrow(int x, int y, List<Rectangle> frames) : base(x, y, frames)
        {
        }

        public override void PerformAction()
        {
            Console.WriteLine("Wooden arrow collected!");
        }
    }
}