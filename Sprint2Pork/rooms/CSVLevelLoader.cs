using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Sprint2Pork.Blocks;

namespace Sprint2Pork.rooms
{
    public class CSVLevelLoader
    {
        public static List<Block> LoadBlocksFromCSV(string filePath, Texture2D texture)
        {
            List<Block> blocks = new List<Block>();
            int tileSize = 16;
            int row = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                string[] cells = line.Split(',');

                for (int col = 0; col < cells.Length; col++)
                {
                    int blockID;
                    if (int.TryParse(cells[col], out blockID) && blockID != 0)
                    {
                        Vector2 position = new Vector2(col * tileSize, row * tileSize);
                        Block block = CreateBlock(blockID, texture, position);
                        if (block != null)
                        {
                            blocks.Add(block);
                        }
                    }
                }

                row++;
            }

            return blocks;
        }

        private static Block CreateBlock(int blockID, Texture2D texture, Vector2 position)
        {
            switch (blockID)
            {
                case 1: return new Block1(texture, position);
                case 2: return new Block2(texture, position);
                case 3: return new Block3(texture, position);
                case 4: return new Block4(texture, position);
                case 5: return new Block5(texture, position);
                case 6: return new Block6(texture, position);
                case 7: return new Block7(texture, position);
                case 8: return new Block8(texture, position);
                case 9: return new Block9(texture, position);
                case 10: return new Block10(texture, position);
                default: return null;
            }
        }
    }
}
