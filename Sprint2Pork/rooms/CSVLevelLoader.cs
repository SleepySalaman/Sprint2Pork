using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Sprint2Pork.Blocks;
using Sprint2Pork.GroundItems;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;

namespace Sprint2Pork.rooms
{
    public class CSVLevelLoader
    {
        public static void LoadObjectsFromCSV(string fileName, Texture2D blockTexture, Texture2D itemTexture, Texture2D enemyTexture,
                                              out List<Block> blocks, out List<GroundItem> groundItems, out List<IEnemy> enemies)
        {
            blocks = new List<Block>();
            groundItems = new List<GroundItem>();
            enemies = new List<IEnemy>();

            int tileSize = 16;
            int row = 0;

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "rooms", "levels", fileName);

            foreach (string line in File.ReadLines(filePath))
            {
                string[] cells = line.Split(',');

                for (int col = 0; col < cells.Length; col++)
                {
                    int cellValue;
                    if (int.TryParse(cells[col], out cellValue))
                    {
                        Vector2 position = new Vector2(col * tileSize, row * tileSize);

                        switch (cellValue)
                        {
                            // Empty
                            case 0:
                                break;

                            // Blocks
                            case 1:
                                blocks.Add(new Block1(blockTexture, position));
                                break;
                            case 2:
                                blocks.Add(new Block2(blockTexture, position));
                                break;
                            case 3:
                                blocks.Add(new Block3(blockTexture, position));
                                break;
                            case 4:
                                blocks.Add(new Block4(blockTexture, position));
                                break;
                            case 5:
                                blocks.Add(new Block5(blockTexture, position));
                                break;
                            case 6:
                                blocks.Add(new Block6(blockTexture, position));
                                break;
                            case 7:
                                blocks.Add(new Block7(blockTexture, position));
                                break;
                            case 8:
                                blocks.Add(new Block8(blockTexture, position));
                                break;
                            case 9:
                                blocks.Add(new Block9(blockTexture, position));
                                break;
                            case 10:
                                blocks.Add(new Block10(blockTexture, position));
                                break;

                            // Ground Items
                            case 11:
                                groundItems.Add(new Rupee((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(72, 0, 8, 16), new Rectangle(72, 16, 8, 16) }));
                                break;
                            case 12:
                                groundItems.Add(new Triangle((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(270, 0, 16, 16), new Rectangle(270, 16, 16, 16) }));
                                break;
                            case 13:
                                groundItems.Add(new Compass((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(256, 0, 16, 16) }));
                                break;
                            case 14:
                                groundItems.Add(new Key((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(248, 0, 8, 16), new Rectangle(240, 0, 8, 16) }));
                                break;
                            case 15:
                                groundItems.Add(new Candle((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(160, 0, 8, 16), new Rectangle(160, 16, 8, 16) }));
                                break;
                            case 16:
                                groundItems.Add(new GroundArrow((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(152, 0, 8, 16), new Rectangle(152, 16, 8, 16) }));
                                break;
                            case 17:
                                groundItems.Add(new Gypsie((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(48, 0, 8, 16), new Rectangle(40, 0, 8, 16) }));
                                break;
                            case 18:
                                groundItems.Add(new Meat((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(96, 0, 8, 16) }));
                                break;
                            case 19:
                                groundItems.Add(new Clock((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(58, 0, 10, 19) }));
                                break;
                            case 20:
                                groundItems.Add(new Potion((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(80, 0, 8, 16), new Rectangle(80, 16, 8, 16) }));
                                break;
                            case 21:
                                groundItems.Add(new Scroll((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(88, 0, 8, 16), new Rectangle(88, 16, 8, 16) }));
                                break;
                            case 22:
                                groundItems.Add(new Heart((int)position.X, (int)position.Y, new List<Rectangle> { new Rectangle(24, 0, 16, 16), new Rectangle(24, 16, 8, 16) }));
                                break;

                            // Enemies
                            case 30:
                                //enemies.Add(new Aquamentus(enemyTexture, position));
                                break;
                                // Add more cases for other enemy types as needed
                        }
                    }
                }
                row++;
            }
        }
    }
}