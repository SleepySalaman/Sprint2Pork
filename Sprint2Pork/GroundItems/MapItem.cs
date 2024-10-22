using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Items
{
    public class MapItem : GroundItem
    {
        public MapItem(int x, int y, List<Rectangle> frames) : base(x, y, frames)
        {
        }

        public override void PerformAction()
        {
            // Logic to increase player's health
            Console.WriteLine("Scroll collected!");
        }
    }
}