using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Constants;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using Sprint2Pork.Managers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sprint2Pork.rooms
{
    public class CSVLevelLoader
    {
        public static void LoadObjectsFromCSV(
            string fileName,
            Texture2D blockTexture,
            Texture2D itemTexture,
            Texture2D enemyTexture,
            out List<Block> blocks,
            out List<GroundItem> groundItems,
            out List<IEnemy> enemies,
            out List<EnemyManager> fireballs,
            SoundManager soundManager)
        {
            blocks = new List<Block>();
            groundItems = new List<GroundItem>();
            enemies = new List<IEnemy>();
            fireballs = new List<EnemyManager>();

            int fireballIDCount = 1;
            int tileSize = GameConstants.BLOCK_TILE_SIZE;
            string filePath = GetFullLevelFilePath(fileName);

            ProcessLevelFile(
                filePath,
                tileSize,
                blocks,
                groundItems,
                enemies,
                fireballs,
                ref fireballIDCount,
                blockTexture,
                soundManager);
        }

        private static string GetFullLevelFilePath(string fileName)
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..", "..", "..",
                "rooms", "levels",
                fileName);
        }

        private static void ProcessLevelFile(
            string filePath,
            int tileSize,
            List<Block> blocks,
            List<GroundItem> groundItems,
            List<IEnemy> enemies,
            List<EnemyManager> fireballs,
            ref int fireballIDCount,
            Texture2D blockTexture,
            SoundManager soundManager)
        {
            int row = 0;
            foreach (string line in File.ReadLines(filePath))
            {
                string[] cells = line.Split(',');
                ProcessRowCells(
                    cells,
                    row,
                    tileSize,
                    blocks,
                    groundItems,
                    enemies,
                    fireballs,
                    ref fireballIDCount,
                    blockTexture,
                    soundManager);
                row++;
            }
        }

        private static void ProcessRowCells(
            string[] cells,
            int row,
            int tileSize,
            List<Block> blocks,
            List<GroundItem> groundItems,
            List<IEnemy> enemies,
            List<EnemyManager> fireballs,
            ref int fireballIDCount,
            Texture2D blockTexture,
            SoundManager soundManager)
        {
            for (int col = 0; col < cells.Length; col++)
            {
                int cellValue;
                if (int.TryParse(cells[col], out cellValue))
                {
                    Vector2 position = new Vector2(
                        col * tileSize,
                        row * tileSize * GameConstants.TILE_HEIGHT_MULTIPLIER);

                    ProcessCellValue(
                        cellValue,
                        position,
                        blocks,
                        groundItems,
                        enemies,
                        fireballs,
                        ref fireballIDCount,
                        blockTexture,
                        soundManager);
                }
            }
        }

        private static void ProcessCellValue(
            int cellValue,
            Vector2 position,
            List<Block> blocks,
            List<GroundItem> groundItems,
            List<IEnemy> enemies,
            List<EnemyManager> fireballs,
            ref int fireballIDCount,
            Texture2D blockTexture,
            SoundManager soundManager)
        {
            switch (cellValue)
            {
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
                    blocks.Add(new Block2(blockTexture, position, true));
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
                    groundItems.Add(CreateRupee(position));
                    break;
                case 12:
                    groundItems.Add(CreateTriangle(position));
                    break;
                case 13:
                    groundItems.Add(CreateCompass(position));
                    break;
                case 14:
                    groundItems.Add(CreateKey(position));
                    break;
                case 15:
                    groundItems.Add(CreateCandle(position));
                    break;
                case 17:
                    groundItems.Add(CreateGypsie(position));
                    break;
                case 18:
                    groundItems.Add(CreateMeat(position));
                    break;
                case 19:
                    groundItems.Add(CreateClock(position));
                    break;
                case 20:
                    groundItems.Add(CreatePotion(position));
                    break;
                case 21:
                    groundItems.Add(CreateMapItem(position));
                    break;
                case 22:
                    groundItems.Add(CreateHeart(position));
                    break;
                case 23:
                    groundItems.Add(CreateGroundBomb(position));
                    break;
                case 24:
                    groundItems.Add(CreatePig(position));
                    break;
                case 25:
                    groundItems.Add(CreatePorkSwordGround(position));
                    break;


                // Enemies
                case 30:
                    CreateAquamentus(position, enemies, fireballs, ref fireballIDCount, soundManager);
                    break;
                case 31:
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
                    enemies.Add(new Gel((int)position.X, (int)position.Y));
                    break;
                case 36:
                    enemies.Add(new Gleeok((int)position.X, (int)position.Y));
                    break;
                case 37:
                    enemies.Add(new Gohma((int)position.X, (int)position.Y));
                    break;
                case 38:
                    enemies.Add(new Goriya((int)position.X, (int)position.Y));
                    break;
                case 39:
                    enemies.Add(new Manhandla((int)position.X, (int)position.Y));
                    break;
                case 40:
                    enemies.Add(new Stalfos((int)position.X, (int)position.Y));
                    break;
                case 41:
                    enemies.Add(new Wizard((int)position.X, (int)position.Y));
                    break;
            }
        }

        // Helper methods for creating ground items
        private static Rupee CreateRupee(Vector2 position) =>
            new Rupee((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(72, 0, 8, 16),
                new Rectangle(72, 16, 8, 16)
            });

        private static Triangle CreateTriangle(Vector2 position) =>
            new Triangle((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(270, 0, 16, 16),
                new Rectangle(270, 16, 16, 16)
            });

        private static Compass CreateCompass(Vector2 position) =>
            new Compass((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(256, 0, 16, 16)
            });

        private static Key CreateKey(Vector2 position) =>
            new Key((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(248, 0, 8, 16),
                new Rectangle(240, 0, 8, 16)
            });

        private static Candle CreateCandle(Vector2 position) =>
            new Candle((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(160, 0, 8, 16),
                new Rectangle(160, 16, 8, 16)
            });

        private static Gypsie CreateGypsie(Vector2 position) =>
            new Gypsie((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(48, 0, 8, 16),
                new Rectangle(40, 0, 8, 16)
            });

        private static Meat CreateMeat(Vector2 position) =>
            new Meat((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(96, 0, 8, 16)
            });

        private static Clock CreateClock(Vector2 position) =>
            new Clock((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(58, 0, 10, 19)
            });

        private static Potion CreatePotion(Vector2 position) =>
            new Potion((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(80, 0, 8, 16),
                new Rectangle(80, 16, 8, 16)
            });

        private static MapItem CreateMapItem(Vector2 position) =>
            new MapItem((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(88, 0, 8, 16),
                new Rectangle(88, 16, 8, 16)
            });

        private static Heart CreateHeart(Vector2 position) =>
            new Heart((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(24, 0, 16, 16),
                new Rectangle(24, 16, 8, 16)
            });

        private static GroundBomb CreateGroundBomb(Vector2 position) =>
            new GroundBomb((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(134, 0, 10, 16),
                new Rectangle(24, 16, 8, 16)
            });

        private static Pig CreatePig(Vector2 position) =>
            new Pig((int)position.X, (int)position.Y, new List<Rectangle> {
                new Rectangle(1, 69, 37, 50)
            });

        private static PorkSwordGround CreatePorkSwordGround(Vector2 position) =>
            new PorkSwordGround((int)position.X, (int)position.Y, new List<Rectangle>
            {
                new Rectangle(104, 0, 7, 16)
            });

        private static void CreateAquamentus(
            Vector2 position,
            List<IEnemy> enemies,
            List<EnemyManager> fireballs,
            ref int fireballIDCount,
            SoundManager soundManager)
        {
            enemies.Add(new Aquamentus((int)position.X, (int)position.Y, fireballIDCount));
            fireballs.Add(new EnemyManager(0, (int)position.X, (int)position.Y, soundManager, fireballIDCount));
            fireballIDCount++;
        }
    }
}