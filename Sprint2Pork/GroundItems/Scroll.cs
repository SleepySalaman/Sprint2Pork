using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Items
{
    public class Scroll : GroundItem
    {
        public Scroll(int x, int y, List<Rectangle> frames) : base(x, y, frames)
        {
        }

        public override void PerformAction()
        {
            // Logic to increase player's health
            Console.WriteLine("Scroll collected!");
        }
    }
}