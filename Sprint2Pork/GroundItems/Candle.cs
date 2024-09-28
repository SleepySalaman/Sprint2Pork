﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sprint2Pork.Items
{
    public class Candle : GroundItem
    {
        public Candle(int x, int y, List<Rectangle> frames) : base(x, y, frames)
        {
        }

        public override void PerformAction()
        {
            Console.WriteLine("Candle collected!");
        }
    }
}