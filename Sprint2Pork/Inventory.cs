﻿using Sprint2Pork.Items;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class Inventory
    {
        private Dictionary<string, int> items;

        public Inventory()
        {
            items = new Dictionary<string, int>
            {
                { "Rupee", 0 },
                { "Key", 0 },
                { "GroundBomb", 0 }
            };
        }

        public void AddItem(GroundItem item, int count = 1)
        {
            string itemName = item.GetType().Name;
            if (items.ContainsKey(itemName))
            {
                items[itemName] += count;
            }
        }

        public int GetItemCount(string itemName)
        {
            var val = items.ContainsKey(itemName) ? items[itemName] : 0;
            return val;
        }

        public void Reset()
        {
            var keys = new List<string>(items.Keys);
            foreach (var key in keys)
            {
                items[key] = 0;
            }
        }
    }
}