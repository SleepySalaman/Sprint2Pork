﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Enemies;
using Sprint2Pork.Enemies.Aquamentus;
using Sprint2Pork.Enemies.Digdogger;
using Sprint2Pork.Enemies.Dodongo;
using Sprint2Pork.Enemies.Gleeok;
using Sprint2Pork.Enemies.Gohma;
using Sprint2Pork.Enemies.Manhandla;
using Sprint2Pork.Enemies.Patra_Ganon;
using Sprint2Pork.Items;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<IController> controllerList;

        private ISprite textSprite;
        private IEnemy enemySprite;

        private Texture2D characterTexture;
        private Texture2D enemyTexture;
        private Texture2D blockTexture;
        private Texture2D itemTexture;

        private SpriteFont font;

        private int[] spritePos;
        private bool moving;

        private double switchCooldown = 0.1;
        private double timeSinceLastSwitch = 0;
        private double switchEnemyCooldown = 0.3;
        private double timeSinceSwitchedEnemy = 0;

        private List<Block> blocks;
        private int currentBlockIndex;
        private Vector2 blockPosition;

        private List<GroundItem> items;
        private int currentItemIndex;

        public enum EnemyList { Aquamentus, Dodongo, Manhandla, Gleeok, Digdogger, Gohma, Ganon };

        private int currentEnemyNum;
        private int numEnemies;

        private Link link;
        public Viewport viewport;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            controllerList = new List<IController>();
            spritePos = new int[2] { 50, 50 };
            currentEnemyNum = 0;
            numEnemies = 7;
            moving = false;

            LoadGroundItems();

            blocks = new List<Block>();
            currentBlockIndex = 0;
            currentItemIndex = 0;
            blockPosition = new Vector2(200, 200);
        }

        private void LoadGroundItems()
        {
            // Load items with predefined frames
            List<Rectangle> rupeeFrames = new List<Rectangle> { new Rectangle(72, 0, 8, 16), new Rectangle(72, 16, 8, 16) };
            List<Rectangle> potionFrames = new List<Rectangle> { new Rectangle(80, 0, 8, 16), new Rectangle(80, 16, 8, 16) };
            List<Rectangle> scrollFrames = new List<Rectangle> { new Rectangle(88, 0, 8, 16), new Rectangle(88, 16, 8, 16) };
            List<Rectangle> heartFrames = new List<Rectangle> { new Rectangle(24, 0, 16, 16), new Rectangle(24, 16, 8, 16) };

            items = new List<GroundItem> {
                new Heart(400, 200, heartFrames),
                new Rupee(400, 200, rupeeFrames),
                new Potion(400, 200, potionFrames),
                new Scroll(400, 200, scrollFrames),
            };
        }

        protected override void Initialize()
        {
            viewport = graphics.GraphicsDevice.Viewport;
            link = new Link(viewport.Width, viewport.Height);

            controllerList.Add(new KeyboardController(this, link));
            controllerList.Add(new MouseController(this)); // Keep MouseController if it's relevant

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            enemySprite = new AquamentusNotAttacking();

            characterTexture = Content.Load<Texture2D>("LinkMovingWithDamage");
            enemyTexture = Content.Load<Texture2D>("zeldaenemies");
            blockTexture = Content.Load<Texture2D>("blocks");
            itemTexture = Content.Load<Texture2D>("items_and_weapons");

            font = Content.Load<SpriteFont>("File");
            textSprite = new TextSprite(200, 100, font);

            // Create blocks
            int numberOfBlocks = 10;
            for (int i = 0; i < numberOfBlocks; i++)
            {
                blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * i, 0, 16, 16)));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            enemySprite.Update();
            timeSinceLastSwitch += gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceSwitchedEnemy += gameTime.ElapsedGameTime.TotalSeconds;

            // Link state update
            link.actionState.Update();
            link.linkSprite.Update(link.x, link.y);

            // Update current item
            items[currentItemIndex].Update(items[currentItemIndex].destinationRect.X, items[currentItemIndex].destinationRect.Y);

            base.Update(gameTime);
        }

        private void ResetGame()
        {
            // Reset the game logic (item/block index, position, etc.)
            currentBlockIndex = 0;
            currentItemIndex = 0;
            spritePos[0] = 50;
            spritePos[1] = 50;
            moving = false;

            enemySprite = new AquamentusNotAttacking();
            link = new Link(viewport.Width, viewport.Height);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.DimGray);

            enemySprite.Draw(spriteBatch, enemyTexture);
            textSprite.Draw(spriteBatch, characterTexture);

            // Draw the current block and item
            blocks[currentBlockIndex].Draw(spriteBatch);
            items[currentItemIndex].Draw(spriteBatch, itemTexture);

            link.Draw(spriteBatch, characterTexture);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void cycleEnemies()
        {
            currentEnemyNum = (currentEnemyNum + 1) % numEnemies;
            setEnemySprite();
        }

        public void cycleEnemiesBackwards()
        {
            currentEnemyNum = (currentEnemyNum - 1 + numEnemies) % numEnemies;
            setEnemySprite();
        }

        public void setEnemySprite()
        {
            switch (currentEnemyNum)
            {
                case 0: enemySprite = new AquamentusNotAttacking(); break;
                case 1: enemySprite = new DodongoIdle(); break;
                case 2: enemySprite = new ManhandlaMoving(); break;
                case 3: enemySprite = new GleeokMoving(); break;
                case 4: enemySprite = new DigdoggerLarge(); break;
                case 5: enemySprite = new GohmaNotAttacking(); break;
                case 6: enemySprite = new GanonNotDamaged(); break;
            }
        }
    }
}
