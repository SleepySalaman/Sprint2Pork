using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.GroundItems;
using Sprint2Pork.Items;
using Sprint2Pork.rooms;
using System;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public bool IsFullscreen { get; set; }
        private SpriteBatch spriteBatch;
        private List<IController> controllerList;

        private ISprite textSprite;
        private IEnemy enemySprite;

        //character, fb, enemy, gel, bat, goriya, wizard, stalfos, blocks, ItemsAndWeapons, ItemsAndWeaponsExpanded
        private List<Texture2D> allTextures;

        private SpriteFont font;

        private int[] spritePos = new int[2] { 0, 0 };
        private bool moving = false;

        private double switchCooldown = 0.1;
        private double timeSinceLastSwitch = 0;
        private double switchEnemyCooldown = 0.3;
        private double timeSinceSwitchedEnemy = 0;
        private CSVLevelLoader csvLevelLoader;
        private List<Block> blocks;
        private int currentBlockIndex = 0;
        private Vector2 blockPosition;

        private List<GroundItem> items;
        private int currentItemIndex = 0;

        private EnemyManager enemyManager;

        private int currentEnemyNum = 0;
        private int numEnemies = 12;

        private Collision collisionHandler;

        private Link link;
        public Viewport viewport;

        public bool menu = false;
        private string currentRoom;
        private Dictionary<string, List<Block>> rooms;

        public int CurrentBlockIndex
        {
            get => currentBlockIndex;
            set
            {
                currentBlockIndex = value;
                UpdateCurrentBlock();
            }
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFullscreen = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            allTextures = new List<Texture2D>();
            blocks = new List<Block>();
            blockPosition = new Vector2(200, 200);

            controllerList = new List<IController>();
            csvLevelLoader = new CSVLevelLoader();
            LoadGroundItems();

            rooms = new Dictionary<string, List<Block>>();

        }

        private void LoadGroundItems()
        {
            // Load items with predefined frames
            GroundItemsController itemController = new GroundItemsController();
            items = itemController.createGroundItems();
        }

        protected override void Initialize()
        {
            viewport = graphics.GraphicsDevice.Viewport;
            link = new Link(viewport.Width, viewport.Height);
            controllerList.Add(new KeyboardController(this, link, blocks));

            controllerList.Add(new MouseController(this)); // Keep MouseController if it's relevant

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            enemySprite = new Aquamentus();
            enemyManager = new EnemyManager(enemySprite.getX());

            LoadTextures.loadAllTextures(allTextures, Content);

            font = Content.Load<SpriteFont>("File");
            textSprite = new TextSprite(200, 100, font);

            GenerateBlocks.fillBlockList(blocks, allTextures[8], blockPosition);

            Texture2D blockTexture = allTextures[8];
            rooms["room1"] = CSVLevelLoader.LoadBlocksFromCSV("room1.csv", blockTexture);

            currentRoom = "room1";
            blocks = rooms[currentRoom];
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            enemySprite.Update();
            enemySprite.Move();
            if(currentEnemyNum == 0) {
                enemyManager.Update(gameTime, enemySprite.getX());
            }

            timeSinceLastSwitch += gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceSwitchedEnemy += gameTime.ElapsedGameTime.TotalSeconds;

            link.actionState.Update();
            link.linkSprite.Update(link.x, link.y);
            if (link.itemInUse)
            {
                link.linkItem.Update(link);
            }

            items[currentItemIndex].Update(items[currentItemIndex].destinationRect.X, items[currentItemIndex].destinationRect.Y);
            
            base.Update(gameTime);
        }
        public void UpdateCurrentBlock()
        {
            currentBlockIndex = CurrentBlockIndex;

        }

        public void PreviousItem()
        {
            currentItemIndex = (currentItemIndex - 1 + items.Count) % items.Count;
            SetCurrentItem();
        }

        public void NextItem()
        {
            currentItemIndex = (currentItemIndex + 1) % items.Count;
            SetCurrentItem();
        }

        private void SetCurrentItem()
        {
            GroundItem currentItem = items[currentItemIndex];
            currentItem.destinationRect = new Rectangle(400, 200, 32, 32);
        }
        public void ResetGame()
        {
            currentBlockIndex = 0;
            currentItemIndex = 0;
            spritePos[0] = 50;
            spritePos[1] = 50;
            moving = false;

            enemySprite = new Aquamentus();
            enemyManager = new EnemyManager(enemySprite.getX());
            currentEnemyNum = 0;

            link = new Link(viewport.Width, viewport.Height);

            foreach (IController controller in controllerList)
            {
                if (controller is KeyboardController keyboardController)
                {
                    keyboardController.UpdateLink(link);
                }
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.DodgerBlue);

            drawCurrentEnemy();
            textSprite.Draw(spriteBatch, allTextures[0]);

            blocks[CurrentBlockIndex].Draw(spriteBatch);
            items[currentItemIndex].Draw(spriteBatch, allTextures[9]);

            link.Draw(spriteBatch, allTextures[0], allTextures[10]);
            enemyManager.Draw(spriteBatch, allTextures[1]);

            foreach (Block block in blocks)
            {
                block.Draw(spriteBatch);
            }

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
                case 0: enemySprite = new Aquamentus(); enemyManager = new EnemyManager(enemySprite.getX());  break;
                case 1: enemySprite = new Dodongo(); enemyManager.clearFireballs();  break;
                case 2: enemySprite = new Manhandla(); break;
                case 3: enemySprite = new Gleeok(); break;
                case 4: enemySprite = new Digdogger(); break;
                case 5: enemySprite = new Gohma(); break;
                case 6: enemySprite = new Ganon(); break;
                case 7: enemySprite = new Gel(); break;
                case 8: enemySprite = new Bat(); break;
                case 9: enemySprite = new Goriya(); break;
                case 10: enemySprite = new Wizard(); break;
                case 11: enemySprite = new Stalfos(); enemyManager.clearFireballs(); break;
            }
        }

        public void drawCurrentEnemy() {
            if (currentEnemyNum < 7) {
                enemySprite.Draw(spriteBatch, allTextures[2]);
            } else {
                enemySprite.Draw(spriteBatch, allTextures[currentEnemyNum - 4]);
            } 
        }

        public void SwitchRoom(string roomName)
        {
            if (rooms.ContainsKey(roomName))
            {
                currentRoom = roomName;
                blocks = rooms[currentRoom];
            }
        }
        /*
        Example of switching rooms when the player reaches a certain position
        if (link.Position.X > someThreshold)
        {
            SwitchRoom("room2");
        }
        */
    }
}
