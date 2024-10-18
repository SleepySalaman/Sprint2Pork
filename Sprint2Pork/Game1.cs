﻿using Microsoft.Xna.Framework;
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
        private Texture2D backgroundTexture;

        private double switchCooldown = 0.1;
        private double timeSinceLastSwitch = 0;
        private double switchEnemyCooldown = 0.3;
        private double timeSinceSwitchedEnemy = 0;
        private CSVLevelLoader csvLevelLoader;
        private List<Block> blocks;
        private List<GroundItem> groundItems;
        private List<IEnemy> enemies;
        private List<EnemyManager> fireballManagers;
        private int enemyInitX = 450;
        private int enemyInitY = 350;
        private int currentBlockIndex = 0;
        private Vector2 blockPosition;

        private List<GroundItem> items;
        private int currentItemIndex = 0;

        private EnemyManager enemyManager;
        private UpdateEnemySprite enemyUpdater;

        private int currentEnemyNum = 0;
        private int numEnemies = 12;

        private Collision collisionHandler;
        private RoomChange roomChanger;

        private Link link;
        public Viewport viewport;

        public bool menu = false;
        private string currentRoom;
        private Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms;

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
            fireballManagers = new List<EnemyManager>();
            blockPosition = new Vector2(200, 200);

            controllerList = new List<IController>();
            csvLevelLoader = new CSVLevelLoader();
            collisionHandler = new Collision();
            roomChanger = new RoomChange();
            LoadGroundItems();

            rooms = new Dictionary<string, (List<Block> blocks, List<GroundItem> groundItems, List<IEnemy> enemies, List<EnemyManager> fireballs)>();
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
            enemyUpdater = new UpdateEnemySprite(enemyInitX, enemyInitY);
            enemyManager = new EnemyManager(enemySprite.getX(), enemyInitX, enemyInitY);

            LoadTextures.loadAllTextures(allTextures, Content);

            font = Content.Load<SpriteFont>("File");
            backgroundTexture = Content.Load<Texture2D>("Background");
            textSprite = new TextSprite(200, 100, font);

            GenerateBlocks.fillBlockList(blocks, allTextures[8], blockPosition);

            Texture2D blockTexture = allTextures[8];
            Texture2D groundItemTexture = allTextures[9];
            Texture2D enemyTexture = allTextures[2];
            LoadRooms(blockTexture, groundItemTexture, enemyTexture);
        }

        private void LoadRooms(Texture2D blockTexture, Texture2D groundItemTexture, Texture2D enemyTexture)
        {
            CSVLevelLoader.LoadObjectsFromCSV("room1.csv", blockTexture, groundItemTexture, enemyTexture, out var room1Blocks, out var room1Items, out var room1Enemies, out var fireballManagerRoom1);
            CSVLevelLoader.LoadObjectsFromCSV("room2.csv", blockTexture, groundItemTexture, enemyTexture, out var room2Blocks, out var room2Items, out var room2Enemies, out var fireballManagersRoom2);

            rooms["room1"] = (new List<Block>(room1Blocks), new List<GroundItem>(room1Items), new List<IEnemy>(room1Enemies), new List<EnemyManager>(fireballManagerRoom1));
            rooms["room2"] = (new List<Block>(room2Blocks), new List<GroundItem>(room2Items), new List<IEnemy>(room2Enemies), new List<EnemyManager>(fireballManagersRoom2));
            currentRoom = "room1";
            (blocks, groundItems, enemies, fireballManagers) = rooms[currentRoom];
        }

        protected override void Update(GameTime gameTime)
        {
            int linkPreviousX = link.X;
            int linkPreviousY = link.Y;

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
            foreach (var enemy in enemies) {
                enemy.Update();
                enemy.Move();
                bool collidesWithLink = collisionHandler.Collides(link.GetRect(), enemy.getRect());
                enemy.updateFromCollision(collidesWithLink, Color.Red);
                if (collidesWithLink)
                {
                    
                    link.TakeDamage();
                }
            }
            foreach (var fireball in fireballManagers) {
                fireball.Update(gameTime, 0);
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
                enemySprite.updateFromCollision(collisionHandler.Collides(link.linkItemSprite.GetRect(), enemySprite.getRect()), Color.Red);
            }
            else
            {
                enemySprite.updateFromCollision(false, Color.White);
            }

            link.actionState.Update();
            link.linkSprite.Update(link.X, link.Y);
            if (link.ItemInUse)
            {
                link.linkItem.Update(link);
            }

            HandleBlockCollision(linkPreviousX, linkPreviousY);
        }

        private void HandleBlockCollision(int linkPreviousX, int linkPreviousY)
        {
            foreach (Block block in blocks)
            {
                if (collisionHandler.Collides(link.GetRect(), block.getBoundingBox()))
                {
                    link.X = linkPreviousX;
                    link.Y = linkPreviousY;
                    break;
                }
            }
        }

        private void UpdateGroundItems()
        {
            var itemsToRemove = new List<GroundItem>();

            foreach (var item in groundItems)
            {
                item.Update(item.destinationRect.X, item.destinationRect.Y);
                if (collisionHandler.CollidesWithGroundItem(link.GetRect(), item.GetRect()))
                {
                    item.PerformAction();
                    itemsToRemove.Add(item);
                }
            }

            foreach (var item in itemsToRemove)
            {
                groundItems.Remove(item);
            }
        }

        private void CheckRoomChange()
        {
            if (currentRoom == "room1" && link.X > GraphicsDevice.Viewport.Width)
            {
                roomChanger.SwitchRoom("room2", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                link.X = 0;
            }
            // Check if Link has moved off the left side of the screen
            else if (currentRoom == "room2" && link.X <= 0)
            {
                roomChanger.SwitchRoom("room1", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                link.X = GraphicsDevice.Viewport.Width - 1; // Reset Link's position to the right side of the screen
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
            enemyUpdater = new UpdateEnemySprite(enemyInitX, enemyInitY);
            currentEnemyNum = 0;

            link = new Link(viewport.Width, viewport.Height);

            currentRoom = "room1";
            (blocks, groundItems, enemies, fireballManagers) = rooms[currentRoom];

            Texture2D blockTexture = allTextures[8];
            Texture2D groundItemTexture = allTextures[9];
            Texture2D enemyTexture = allTextures[2];

            LoadRooms(blockTexture, groundItemTexture, enemyTexture);

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

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);

            enemyUpdater.drawCurrentEnemy(enemySprite, spriteBatch, allTextures, currentEnemyNum);
            textSprite.Draw(spriteBatch, allTextures[0]);

            //blocks[CurrentBlockIndex].Draw(spriteBatch);
            //items[currentItemIndex].Draw(spriteBatch, allTextures[9]);

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
                enemy.Draw(spriteBatch, allTextures[2]);
            }
            foreach (var fireball in fireballManagers)
            {
                fireball.Draw(spriteBatch, allTextures[1]);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void cycleEnemies()
        {
            currentEnemyNum = (currentEnemyNum + 1) % numEnemies;
            enemyUpdater.setEnemySprite(currentEnemyNum, ref enemySprite, ref enemyManager);
        }

        public void cycleEnemiesBackwards()
        {
            currentEnemyNum = (currentEnemyNum - 1 + numEnemies) % numEnemies;
            enemyUpdater.setEnemySprite(currentEnemyNum, ref enemySprite, ref enemyManager);
        }

        public void GetDevRoom()
        {
            roomChanger.SwitchRoom("room1", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
            link = new Link(viewport.Width, viewport.Height);
            foreach (IController controller in controllerList)
            {
                if (controller is KeyboardController keyboardController)
                {
                    keyboardController.UpdateLink(link);
                }
            }
        }

        public void SwitchToNextRoom() {
            roomChanger.SwitchToNextRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
        }

        public void SwitchToPreviousRoom() {
            roomChanger.SwitchToPreviousRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
        }
    }
}
