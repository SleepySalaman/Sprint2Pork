using Microsoft.Xna.Framework;
using Sprint2Pork.Essentials;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class Paused
    {
        private Inventory inventory;
        private Dictionary<string, Rectangle> itemSourceRects;

        public Paused(Inventory gameInventory)
        {
            inventory = gameInventory ?? new Inventory();
            InitializeItemSourceRects();
        }

        private void InitializeItemSourceRects()
        {
            itemSourceRects = new Dictionary<string, Rectangle>
            {
                { "Rupee", new Rectangle(72, 0, 8, 16) },
                { "Key", new Rectangle(240, 0, 8, 16) },
                { "GroundBomb", new Rectangle(136, 0, 8, 16) }
            };
        }
    }
}
