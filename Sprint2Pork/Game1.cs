using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.GroundItems;
using Sprint2Pork.Items;
using Sprint2Pork.rooms;
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
        private List<GroundItem> groundItems;
        private List<IEnemy> enemies;
        private int enemyInitX = 450;
        private int enemyInitY = 350;
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
        private Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>)> rooms;

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
            groundItems = new List<GroundItem>();
            enemies = new List<IEnemy>();
            blockPosition = new Vector2(200, 200);

            controllerList = new List<IController>();
            csvLevelLoader = new CSVLevelLoader();
            collisionHandler = new Collision();
            LoadGroundItems();

            rooms = new Dictionary<string, (List<Block> blocks, List<GroundItem> groundItems, List<IEnemy> enemies)>();
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
            controllerList.Add(new MouseController(this)); 

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            enemySprite = new Aquamentus(enemyInitX, enemyInitY);
            enemyManager = new EnemyManager(enemySprite.getX(), enemyInitX, enemyInitY);

            LoadTextures.loadAllTextures(allTextures, Content);

            font = Content.Load<SpriteFont>("File");
            textSprite = new TextSprite(200, 100, font);

            GenerateBlocks.fillBlockList(blocks, allTextures[8], blockPosition);

            Texture2D blockTexture = allTextures[8];
            Texture2D groundItemTexture = allTextures[9];
            Texture2D enemyTexture = allTextures[2];

            CSVLevelLoader.LoadObjectsFromCSV("room1.csv", blockTexture, groundItemTexture, enemyTexture, out var room1Blocks, out var room1Items, out var room1Enemies);
            CSVLevelLoader.LoadObjectsFromCSV("room2.csv", blockTexture, groundItemTexture, enemyTexture, out var room2Blocks, out var room2Items, out var room2Enemies);

            rooms["room1"] = (new List<Block>(room1Blocks), new List<GroundItem>(room1Items), new List<IEnemy>(room1Enemies));
            rooms["room2"] = (new List<Block>(room2Blocks), new List<GroundItem>(room2Items), new List<IEnemy>(room2Enemies));
            currentRoom = "room1";
            (blocks, groundItems, enemies) = rooms[currentRoom];
        }

        protected override void Update(GameTime gameTime)
        {
            int linkPreviousX = link.x;
            int linkPreviousY = link.y;

            timeSinceLastSwitch += gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceSwitchedEnemy += gameTime.ElapsedGameTime.TotalSeconds;

            UpdateControllers();
            UpdateGroundItems();
            UpdateEnemies(gameTime);
            UpdateLink(linkPreviousX, linkPreviousY);

            CheckRoomChange();

            base.Update(gameTime);
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            foreach (var enemy in enemies)
            {
                enemy.Update();
                enemy.Move();
                enemy.updateFromCollision(collisionHandler.collides(link.getRect(), enemy.getRect()), Color.Red);
                link.TakeDamage();
            }

            enemySprite.Update();
            enemySprite.Move();
            if (currentEnemyNum == 0)
            {
                enemyManager.Update(gameTime, enemySprite.getX());
            }
        }

        private void UpdateLink(int linkPreviousX, int linkPreviousY)
        {
            if (link.linkItemSprite != null)
            {
                enemySprite.updateFromCollision(collisionHandler.collides(link.linkItemSprite.getRect(), enemySprite.getRect()), Color.Red);
            }
            else
            {
                enemySprite.updateFromCollision(false, Color.White);
            }

            link.actionState.Update();
            link.linkSprite.Update(link.x, link.y);
            if (link.itemInUse)
            {
                link.linkItem.Update(link);
            }

            HandleBlockCollision(linkPreviousX, linkPreviousY);
        }

        private void HandleBlockCollision(int linkPreviousX, int linkPreviousY)
        {
            foreach (Block block in blocks)
            {
                if (collisionHandler.collides(link.getRect(), block.getBoundingBox()))
                {
                    link.x = linkPreviousX;
                    link.y = linkPreviousY;
                    break;
                }
            }
        }

        private void UpdateGroundItems()
        {
            foreach (var item in groundItems)
            {
                item.Update(item.destinationRect.X, item.destinationRect.Y);
            }
        }

        private void CheckRoomChange()
        {
            if (currentRoom == "room1" && link.x > GraphicsDevice.Viewport.Width)
            {
                SwitchRoom("room2");
                link.x = 0;
            }
            // Check if Link has moved off the left side of the screen
            else if (currentRoom == "room2" && link.x <= 0)
            {
                SwitchRoom("room1");
                link.x = GraphicsDevice.Viewport.Width - 1; // Reset Link's position to the right side of the screen
            }
        }

        private void UpdateControllers()
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }
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

            enemySprite = new Aquamentus(enemyInitX, enemyInitY);
            enemyManager = new EnemyManager(enemySprite.getX(), enemyInitX, enemyInitY);
            currentEnemyNum = 0;

            link = new Link(viewport.Width, viewport.Height);

            currentRoom = "room1";
            (blocks, groundItems, enemies) = rooms[currentRoom];

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

            foreach (var item in groundItems)
            {
                item.Draw(spriteBatch, allTextures[9]);
            }

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, allTextures[10]);
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
                case 0: enemySprite = new Aquamentus(enemyInitX, enemyInitY); enemyManager = new EnemyManager(enemySprite.getX(), enemyInitX, enemyInitY); break;
                case 1: enemySprite = new Dodongo(enemyInitX, enemyInitY); enemyManager.clearFireballs(); break;
                case 2: enemySprite = new Manhandla(enemyInitX, enemyInitY); break;
                case 3: enemySprite = new Gleeok(enemyInitX, enemyInitY); break;
                case 4: enemySprite = new Digdogger(enemyInitX, enemyInitY); break;
                case 5: enemySprite = new Gohma(enemyInitX, enemyInitY); break;
                case 6: enemySprite = new Ganon(enemyInitX, enemyInitY); break;
                case 7: enemySprite = new Gel(enemyInitX, enemyInitY); break;
                case 8: enemySprite = new Bat(enemyInitX, enemyInitY); break;
                case 9: enemySprite = new Goriya(enemyInitX, enemyInitY); break;
                case 10: enemySprite = new Wizard(enemyInitX, enemyInitY); break;
                case 11: enemySprite = new Stalfos(enemyInitX, enemyInitY); enemyManager.clearFireballs(); break;
            }
        }

        public void drawCurrentEnemy()
        {
            if (currentEnemyNum < 7)
            {
                enemySprite.Draw(spriteBatch, allTextures[2]);
            }
            else
            {
                enemySprite.Draw(spriteBatch, allTextures[currentEnemyNum - 4]);
            }
        }
        public void GetDevRoom()
        {
            SwitchRoom("room1");
            link.x = 50;
            link.y = 50;
            link.LookDown();
        }
        public void SwitchToNextRoom()
        {
            var roomNames = new List<string>(rooms.Keys);
            int currentIndex = roomNames.IndexOf(currentRoom);
            int nextIndex = (currentIndex + 1) % roomNames.Count;
            SwitchRoom(roomNames[nextIndex]);
        }

        public void SwitchToPreviousRoom()
        {
            var roomNames = new List<string>(rooms.Keys);
            int currentIndex = roomNames.IndexOf(currentRoom);
            int previousIndex = (currentIndex - 1 + roomNames.Count) % roomNames.Count;
            SwitchRoom(roomNames[previousIndex]);
        }

        public void SwitchRoom(string newRoom)
        {
            currentRoom = newRoom;
            (blocks, groundItems, enemies) = rooms[currentRoom];
        }
    }
}
