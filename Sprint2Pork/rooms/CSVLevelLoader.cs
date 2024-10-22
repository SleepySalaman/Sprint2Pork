using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sprint2Pork.rooms
{
    public class CSVLevelLoader
    {
        public static void LoadObjectsFromCSV(string fileName, Texture2D blockTexture, Texture2D itemTexture, Texture2D enemyTexture, out List<Block> blocks, out List<GroundItem> groundItems, out List<IEnemy> enemies, out List<EnemyManager> fireballs)
        {
            blocks = new List<Block>();
            groundItems = new List<GroundItem>();
            enemies = new List<IEnemy>();
            fireballs = new List<EnemyManager>();

            int tileSize = 40;
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
                            case -1:
                                blocks.Add(new InvisibleBlock(blockTexture, position));
                                break;
                            case 1:
                                blocks.Add(new FloorBlock(blockTexture, position));
                                break;
                            case 2:
                                blocks.Add(new Block2(blockTexture, position));
                                break;
                            case 3:
                                blocks.Add(new EnemyBlock1(blockTexture, position));
                                break;
                            case 4:
                                blocks.Add(new Block4(blockTexture, position));
                                break;
                            case 5:
                                blocks.Add(new BlackBlock(blockTexture, position));
                                break;
                            case 6:
                                blocks.Add(new SpeckledBlock(blockTexture, position));
                                break;
                            case 7:
                                blocks.Add(new DarkBlueBlock(blockTexture, position));
                                break;
                            case 8:
                                blocks.Add(new StairBlock(blockTexture, position));
                                break;
                            case 9:
                                blocks.Add(new Block9(blockTexture, position));
                                break;
                            case 10:
                                blocks.Add(new StripedBlock(blockTexture, position));
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
                                enemies.Add(new Aquamentus((int)position.X, (int)position.Y));
                                fireballs.Add(new EnemyManager(0, (int)position.X, (int)position.Y));
                                break;
                            case 31:
                                // wrong sprite sheet being used  allTextures[4]
                                enemies.Add(new Bat((int)position.X, (int)position.Y));
                                break;
                            case 32:
                                enemies.Add(new Digdogger((int)position.X, (int)position.Y));
                                break;
                            case 33:
                                enemies.Add(new Dodongo((int)position.X, (int)position.Y));
                                break;
                            case 34:
                                enemies.Add(new Ganon((int)position.X, (int)position.Y));
                                break;
                            case 35:
                                // wrong sprite sheet being used  allTextures[3]
                                enemies.Add(new Gel((int)position.X, (int)position.Y));
                                break;
                            case 36:
                                enemies.Add(new Gleeok((int)position.X, (int)position.Y));
                                break;
                            case 37:
                                enemies.Add(new Gohma((int)position.X, (int)position.Y));
                                break;
                            case 38:
                                // wrong sprite sheet being used   allTextures[5]
                                enemies.Add(new Goriya((int)position.X, (int)position.Y));
                                break;
                            case 39:
                                enemies.Add(new Manhandla((int)position.X, (int)position.Y));
                                break;
                            case 40:
                                // wrong sprite sheet being used    allTextures[7]
                                enemies.Add(new Stalfos((int)position.X, (int)position.Y));
                                break;
                            case 41:
                                // wrong sprite sheet being used    allTextures[6]
                                enemies.Add(new Wizard((int)position.X, (int)position.Y));
                                break;
                        }
                    }
                }
                row++;
            }
        }
    }
}