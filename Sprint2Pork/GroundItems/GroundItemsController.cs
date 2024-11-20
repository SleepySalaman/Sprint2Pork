using Microsoft.Xna.Framework;
using Sprint2Pork.Items;
using System.Collections.Generic;

namespace Sprint2Pork.GroundItems
{
    public class GroundItemsController
    {

        //rupee, triangle, compass, key, candle, arrow, gypsie, meat, clock, potion, scroll, heart
        private List<List<Rectangle>> itemFrames;

        public GroundItemsController()
        {
            itemFrames = new List<List<Rectangle>> {
                new List<Rectangle> { new Rectangle(72, 0, 8, 16), new Rectangle(72, 16, 8, 16) },
                new List<Rectangle> { new Rectangle(270, 0, 16, 16), new Rectangle(270, 16, 16, 16) },
                new List<Rectangle> { new Rectangle(256, 0, 16, 16)},
                new List<Rectangle> { new Rectangle(248, 0, 8, 16), new Rectangle(240, 0, 8, 16) },
                new List<Rectangle> { new Rectangle(160, 0, 8, 16), new Rectangle(160, 16, 8, 16) },
                new List<Rectangle> { new Rectangle(152, 0, 8, 16), new Rectangle(24, 16, 8, 16) }, 
                new List<Rectangle> {new Rectangle(152, 16, 8, 16), new Rectangle(24, 16, 8, 16) },
                new List<Rectangle> { new Rectangle(48, 0, 8, 16), new Rectangle(40, 0, 8, 16) },
                new List<Rectangle> { new Rectangle(96, 0, 8, 16) },
                new List<Rectangle> { new Rectangle(58, 0, 10, 19) },
                new List<Rectangle> { new Rectangle(80, 0, 8, 16), new Rectangle(80, 16, 8, 16) },
                new List<Rectangle> { new Rectangle(88, 0, 8, 16), new Rectangle(88, 16, 8, 16) },
                new List<Rectangle> { new Rectangle(24, 0, 16, 16), new Rectangle(24, 16, 8, 16) },
                new List<Rectangle> { new Rectangle(134, 0, 10, 16), new Rectangle(24, 16, 8, 16) },
            };
        }

        public List<GroundItem> createGroundItems()
        {
            List<GroundItem> items = new List<GroundItem> {
                new Rupee(400, 200, itemFrames[0]),
                new Triangle(400, 200, itemFrames[1]),
                new Compass(400, 200, itemFrames[2]),
                new Key(400, 200, itemFrames[3]),
                new Candle(400, 200, itemFrames[4]),
                new Gypsie(400, 200, itemFrames[7]),
                new Meat(400, 200, itemFrames[8]),
                new Clock(400, 200, itemFrames[9]),
                new Potion(400, 200, itemFrames[10]),
                new MapItem(400, 200, itemFrames[11]),
                new Heart(400, 200, itemFrames[12]),
                new GroundBomb(400, 200, itemFrames[13]),
            };
            return items;
        }

    }
}
