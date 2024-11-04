﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.GroundItems;
using Sprint2Pork.Items;
using Sprint2Pork.rooms;
using System.Collections.Generic;
using System;

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
        private Texture2D hudTexture;
        private Texture2D roomTexture;
        private Texture2D nextRoomTexture;

        private double timeSinceLastSwitch = 0;
        private double timeSinceSwitchedEnemy = 0;
        private List<Block> blocks;
        private List<GroundItem> groundItems;
        private List<IEnemy> enemies;
        private List<EnemyManager> fireballManagers;
        private Vector2 enemyInitPos = new Vector2(450, 320);

        private int currentBlockIndex = 0;
        private Vector2 blockPosition = new Vector2(200, 200);

        private List<GroundItem> items;
        private int currentItemIndex = 0;

        private EnemyManager enemyManager;
        private UpdateEnemySprite enemyUpdater;

        private int currentEnemyNum = 0;
        private int numEnemies = 12;

        private Link link;
        public Viewport viewport;

        public bool menu = false;
        private string currentRoom;
        private string nextRoom;
        private Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms;

        // SFX
        private SoundManager soundManager;

        // Game States
        private Game1State gameState;
        private Game1StateManager gameStateManager;
        private float transitionDuration = 1.0f;
        private float transitionTimer = 0f;
        private Rectangle oldRoomRectangle;
        private Rectangle oldRoomRectangleSaved;
        private Rectangle nextRoomRectangle;
        private Rectangle nextRoomRectangleSaved;
        private Vector2 transitionDirection;

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

            soundManager = new SoundManager();
            allTextures = new List<Texture2D>();
            blocks = new List<Block>();
            groundItems = new List<GroundItem>();
            enemies = new List<IEnemy>();
            fireballManagers = new List<EnemyManager>();

            controllerList = new List<IController>();
            LoadGroundItems();

            rooms = new Dictionary<string, (List<Block> blocks, List<GroundItem> groundItems,
                List<IEnemy> enemies, List<EnemyManager> fireballs)>();

            gameState = Game1State.Playing;
            gameStateManager = new Game1StateManager(gameState);
        }

        private void LoadGroundItems()
        {
            // Load items with predefined frames
            GroundItemsController itemController = new GroundItemsController();
            items = itemController.createGroundItems();
        }

        protected override void Initialize(){
            InitializeHandler.baseInitialize(ref viewport, graphics, ref link, ref controllerList, ref blocks, this, soundManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {

            //Loading Sounds
            soundManager.LoadAllSounds(Content);

            InitializeHandler.loadEnemyContent(ref spriteBatch, ref enemySprite, ref enemyUpdater, ref enemyManager, 
                GraphicsDevice, (int)enemyInitPos.X, (int)enemyInitPos.Y, this.soundManager);

            LoadTextures.loadAllTextures(allTextures, Content);

            font = Content.Load<SpriteFont>("File");
            // Loading the background/room
            backgroundTexture = Content.Load<Texture2D>("Room1");
            hudTexture = Content.Load<Texture2D>("ZeldaHUD");
            roomTexture = Content.Load<Texture2D>("Room1Alone");
            textSprite = new TextSprite(200, 100, font);

            GenerateBlocks.fillBlockList(blocks, allTextures[8], blockPosition);

            //blockTexture, groundItemTexture, enemyTexture
            LoadRooms(allTextures[8], allTextures[9], allTextures[2]);
        }

        private void LoadRooms(Texture2D blockTexture, Texture2D groundItemTexture, Texture2D enemyTexture)
        {
            CSVLevelLoader.LoadObjectsFromCSV("room1.csv", blockTexture, groundItemTexture, enemyTexture, out var room1Blocks, out var room1Items, out var room1Enemies, out var fireballManagerRoom1, this.soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room2.csv", blockTexture, groundItemTexture, enemyTexture, out var room2Blocks, out var room2Items, out var room2Enemies, out var fireballManagersRoom2, this.soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room3.csv", blockTexture, groundItemTexture, enemyTexture, out var room3Blocks, out var room3Items, out var room3Enemies, out var fireballManagersRoom3, this.soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room4.csv", blockTexture, groundItemTexture, enemyTexture, out var room4Blocks, out var room4Items, out var room4Enemies, out var fireballManagersRoom4, this.soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room5.csv", blockTexture, groundItemTexture, enemyTexture, out var room5Blocks, out var room5Items, out var room5Enemies, out var fireballManagersRoom5, this.soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room6.csv", blockTexture, groundItemTexture, enemyTexture, out var room6Blocks, out var room6Items, out var room6Enemies, out var fireballManagersRoom6, this.soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room7.csv", blockTexture, groundItemTexture, enemyTexture, out var room7Blocks, out var room7Items, out var room7Enemies, out var fireballManagersRoom7, this.soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room8.csv", blockTexture, groundItemTexture, enemyTexture, out var room8Blocks, out var room8Items, out var room8Enemies, out var fireballManagersRoom8, this.soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room9.csv", blockTexture, groundItemTexture, enemyTexture, out var room9Blocks, out var room9Items, out var room9Enemies, out var fireballManagersRoom9, this.soundManager);

            rooms["room1"] = (new List<Block>(room1Blocks), new List<GroundItem>(room1Items), new List<IEnemy>(room1Enemies), new List<EnemyManager>(fireballManagerRoom1));
            rooms["room2"] = (new List<Block>(room2Blocks), new List<GroundItem>(room2Items), new List<IEnemy>(room2Enemies), new List<EnemyManager>(fireballManagersRoom2));
            rooms["room3"] = (new List<Block>(room3Blocks), new List<GroundItem>(room3Items), new List<IEnemy>(room3Enemies), new List<EnemyManager>(fireballManagersRoom3));
            rooms["room4"] = (new List<Block>(room4Blocks), new List<GroundItem>(room4Items), new List<IEnemy>(room4Enemies), new List<EnemyManager>(fireballManagersRoom4));
            rooms["room5"] = (new List<Block>(room5Blocks), new List<GroundItem>(room5Items), new List<IEnemy>(room5Enemies), new List<EnemyManager>(fireballManagersRoom5));
            rooms["room6"] = (new List<Block>(room6Blocks), new List<GroundItem>(room6Items), new List<IEnemy>(room6Enemies), new List<EnemyManager>(fireballManagersRoom6));
            rooms["room7"] = (new List<Block>(room7Blocks), new List<GroundItem>(room7Items), new List<IEnemy>(room7Enemies), new List<EnemyManager>(fireballManagersRoom7));
            rooms["room8"] = (new List<Block>(room8Blocks), new List<GroundItem>(room8Items), new List<IEnemy>(room8Enemies), new List<EnemyManager>(fireballManagersRoom8));
            rooms["room9"] = (new List<Block>(room9Blocks), new List<GroundItem>(room9Items), new List<IEnemy>(room9Enemies), new List<EnemyManager>(fireballManagersRoom9));

            currentRoom = "room1";
            (blocks, groundItems, enemies, fireballManagers) = rooms[currentRoom];
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameState == Game1State.Playing)
            {
                int linkPreviousX = link.GetX();
                int linkPreviousY = link.GetY();

                timeSinceLastSwitch += gameTime.ElapsedGameTime.TotalSeconds;
                timeSinceSwitchedEnemy += gameTime.ElapsedGameTime.TotalSeconds;

                // Here is the logic that will need to be moved into a StateManager class; put into a switch
                UpdateControllers();
                UpdateGroundItems();
                UpdateEnemies(gameTime);
                UpdateLink(linkPreviousX, linkPreviousY);

                CheckRoomChange(gameState);

                base.Update(gameTime);
            } else if (gameState == Game1State.Transitioning)
            {
                HandleTransition(gameTime);
            }
            
        }

        private void HandleTransition(GameTime gameTime)
        {
            transitionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            int transitionConstant = (int)(Math.Abs(transitionDirection.X) * (transitionTimer * 880.0f)) + (int)(Math.Abs(transitionDirection.Y) * (transitionTimer * 25.0f));

            oldRoomRectangle = new Rectangle(oldRoomRectangleSaved.X - ((int)transitionDirection.X * transitionConstant), oldRoomRectangle.Y - ((int)transitionDirection.Y * transitionConstant), oldRoomRectangle.Width, oldRoomRectangle.Height);
            nextRoomRectangle = new Rectangle(nextRoomRectangleSaved.X - ((int)transitionDirection.X * transitionConstant), nextRoomRectangle.Y - ((int)transitionDirection.Y * transitionConstant), nextRoomRectangle.Width, nextRoomRectangle.Height);

            if (transitionTimer >= transitionDuration || ((0 >= ((int)transitionDirection.X * nextRoomRectangle.X)) && (0 >= ((int)transitionDirection.Y * nextRoomRectangle.Y) - (int)transitionDirection.Y*85)))
            {
                transitionTimer = 0f;
                gameState = Game1State.Playing;
                roomTexture = nextRoomTexture;
                RoomChange.SwitchRoom(nextRoom, ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                currentRoom = nextRoom;

            }
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            EnemyUpdater.updateEnemies(ref link, enemies, blocks);
            EnemyUpdater.updateFireballs(enemyManager, ref link, ref fireballManagers, gameTime);

            enemySprite.Update();
            enemySprite.Move(blocks);
            if (currentEnemyNum == 0)
            {
                enemyManager.Update(gameTime, enemySprite.getX());
            }
        }

        private void UpdateLink(int linkPreviousX, int linkPreviousY)
        {
            ISprite itemSprite = link.linkItem.SpriteGet();
            if (itemSprite != null)
            {
                enemySprite.updateFromCollision(Collision.Collides(itemSprite.GetRect(), enemySprite.getRect()), Color.Red);
                link.linkItem.SpriteSet(itemSprite);
            }  else {
                enemySprite.updateFromCollision(false, Color.White);
            }

            foreach(Enemy e in enemies) {
                if(Collision.Collides(e.getRect(), link.GetRect())) {
                    link.BeDamaged();
                }
            }

            if(Collision.Collides(enemySprite.getRect(), link.GetRect())) {
                link.BeDamaged();
            }

            link.actionState.Update();
            link.LinkSpriteUpdate();
            if (link.IsLinkUsingItem()){
                link.linkItem.Update(link);
            }

            HandleBlockCollision(linkPreviousX, linkPreviousY);
        }

        private void HandleBlockCollision(int linkPreviousX, int linkPreviousY)
        {
            foreach (Block block in blocks)
            {
                if (Collision.Collides(link.GetRect(), block.getBoundingBox()))
                {
                    link.SetX(linkPreviousX);
                    link.SetY(linkPreviousY);
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
                if (Collision.CollidesWithGroundItem(link.GetRect(), item.GetRect()))
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

        private void CheckRoomChange(Game1State gameState)
        {
            if (currentRoom == "room1" && link.GetX() > GraphicsDevice.Viewport.Width - 100)
            {
                nextRoom = "room2";
                nextRoomTexture = Content.Load<Texture2D>("Room2Alone");
                link.SetX(100);

                transitionDirection = new Vector2(1, 0);
                SetRectangles();
                this.gameState = Game1State.Transitioning;


                //RoomChange.SwitchRoom("room2", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);


                //currentRoom = "room2";

            }

            else if (currentRoom == "room1" && link.GetX() < 100)
            {
                nextRoom = "room5";
                nextRoomTexture = Content.Load<Texture2D>("Background");
                transitionDirection = new Vector2(-1, 0);
                SetRectangles();
                link.SetX(GraphicsDevice.Viewport.Width - 101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }

            else if (currentRoom == "room2" && link.GetX() > GraphicsDevice.Viewport.Width - 100)
            {
                nextRoom = "room4";
                nextRoomTexture = Content.Load<Texture2D>("Room4");
                link.SetX(100);

                transitionDirection = new Vector2(1, 0);
                SetRectangles();
                this.gameState = Game1State.Transitioning;
            }

            else if (currentRoom == "room5" && link.GetX() > GraphicsDevice.Viewport.Width - 100)
            {
                nextRoom = "room1";
                nextRoomTexture = Content.Load<Texture2D>("Room1Alone");
                link.SetX(100);

                transitionDirection = new Vector2(1, 0);
                SetRectangles();
                this.gameState = Game1State.Transitioning;
            }

            else if (currentRoom == "room2" && link.GetX() < 100)
            {
                nextRoom = "room1";
                nextRoomTexture = Content.Load<Texture2D>("Room1Alone");
                transitionDirection = new Vector2(-1, 0);
                SetRectangles();
                link.SetX(GraphicsDevice.Viewport.Width - 101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }

            else if (currentRoom == "room4" && link.GetX() < 100)
            {
                nextRoom = "room2";
                nextRoomTexture = Content.Load<Texture2D>("Room2Alone");


                //RoomChange.SwitchRoom("room1", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                transitionDirection = new Vector2(-1, 0);
                SetRectangles();
                link.SetX(GraphicsDevice.Viewport.Width - 101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }

            else if (currentRoom == "room2" && link.GetY() < 100)
            {
                nextRoom = "room3";
                nextRoomTexture = Content.Load<Texture2D>("Room3");
                //RoomChange.SwitchRoom("room3", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                transitionDirection = new Vector2(0, -1);
                SetRectangles();
                link.SetY(GraphicsDevice.Viewport.Height - 99);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }

            else if (currentRoom == "room3" && link.GetY() > GraphicsDevice.Viewport.Height - 30)
            {
                nextRoom = "room2";
                nextRoomTexture = Content.Load<Texture2D>("Room2Alone");
                transitionDirection = new Vector2(0, 1);
                SetRectangles();
                //RoomChange.SwitchRoom("room2", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);

                link.SetY(101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room5" && link.GetY() > GraphicsDevice.Viewport.Height - 30)
            {
                nextRoom = "room6";
                nextRoomTexture = Content.Load<Texture2D>("Background");
                transitionDirection = new Vector2(0, 1);
                SetRectangles();
                //RoomChange.SwitchRoom("room2", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);

                link.SetY(101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room6" && link.GetY() < 100)
            {
                nextRoom = "room5";
                nextRoomTexture = Content.Load<Texture2D>("Room2Alone");
                //RoomChange.SwitchRoom("room3", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                transitionDirection = new Vector2(0, -1);
                SetRectangles();
                link.SetY(GraphicsDevice.Viewport.Height - 99);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room6" && link.GetY() > GraphicsDevice.Viewport.Height - 30)
            {
                nextRoom = "room7";
                nextRoomTexture = Content.Load<Texture2D>("Background");
                transitionDirection = new Vector2(0, 1);
                SetRectangles();
                //RoomChange.SwitchRoom("room2", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);

                link.SetY(101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room7" && link.GetY() < 100)
            {
                nextRoom = "room6";
                nextRoomTexture = Content.Load<Texture2D>("Room2Alone");
                //RoomChange.SwitchRoom("room3", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                transitionDirection = new Vector2(0, -1);
                SetRectangles();
                link.SetY(GraphicsDevice.Viewport.Height - 99);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room5" && link.GetX() < 100)
            {
                nextRoom = "room8";
                nextRoomTexture = Content.Load<Texture2D>("Background");
                transitionDirection = new Vector2(-1, 0);
                SetRectangles();
                link.SetX(GraphicsDevice.Viewport.Width - 101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room8" && link.GetX() > GraphicsDevice.Viewport.Width - 100)
            {
                nextRoom = "room5";
                nextRoomTexture = Content.Load<Texture2D>("Background");
                link.SetX(100);

                transitionDirection = new Vector2(1, 0);
                SetRectangles();
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room8" && link.GetY() < 100)
            {
                nextRoom = "room9";
                nextRoomTexture = Content.Load<Texture2D>("Background");
                //RoomChange.SwitchRoom("room3", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                transitionDirection = new Vector2(0, -1);
                SetRectangles();
                link.SetY(GraphicsDevice.Viewport.Height - 99);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }

            else if (currentRoom == "room9" && link.GetY() > GraphicsDevice.Viewport.Height - 30)
            {
                nextRoom = "room8";
                nextRoomTexture = Content.Load<Texture2D>("Background");
                transitionDirection = new Vector2(0, 1);
                SetRectangles();
                //RoomChange.SwitchRoom("room2", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);

                link.SetY(101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
        }

        private void SetRectangles()
        {
            oldRoomRectangle = new Rectangle(0, 85, viewport.Width, viewport.Height - 85);
            oldRoomRectangleSaved = oldRoomRectangle;
            nextRoomRectangle = new Rectangle(((int)transitionDirection.X * viewport.Width), 85 + ((int)transitionDirection.Y * viewport.Height), viewport.Width, viewport.Height - 85);
            nextRoomRectangleSaved = nextRoomRectangle;
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

            enemySprite = new Aquamentus((int)enemyInitPos.X, (int)enemyInitPos.Y);
            enemyManager = new EnemyManager(enemySprite.getX(), (int)enemyInitPos.X, (int)enemyInitPos.Y, this.soundManager);
            enemyUpdater = new UpdateEnemySprite((int)enemyInitPos.X, (int)enemyInitPos.Y);
            currentEnemyNum = 0;

            soundManager = new SoundManager();
            soundManager.LoadAllSounds(Content);
            link = new Link(viewport.Width, viewport.Height, soundManager);

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

        //public SoundManager RequestSoundManager()
        //{
        //    return this.soundManager;
        //}

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            GraphicsDevice.Clear(Color.Black);

            // Drawing the background/room
            //spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);
            spriteBatch.Draw(hudTexture, new Rectangle(0, 0, viewport.Width, 89), Color.White);
            if (gameState == Game1State.Playing)
            {
                spriteBatch.Draw(roomTexture, new Rectangle(0, 85, viewport.Width, viewport.Height - 85), Color.White);

                //blocks[CurrentBlockIndex].Draw(spriteBatch);
                //items[currentItemIndex].Draw(spriteBatch, allTextures[9]);

                link.Draw(spriteBatch, allTextures[0], allTextures[10]);

                Drawing.DrawCyclingEnemy(enemyUpdater, enemyManager, spriteBatch, allTextures, enemySprite, currentEnemyNum, textSprite);
                Drawing.DrawGeneratedObjects(spriteBatch, blocks, groundItems, enemies, fireballManagers, allTextures);
            } else if (gameState == Game1State.Transitioning)
            {
                spriteBatch.Draw(roomTexture, oldRoomRectangle, Color.White);
                spriteBatch.Draw(nextRoomTexture, nextRoomRectangle, Color.White);
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void cycleEnemies()
        {
            currentEnemyNum = (currentEnemyNum + 1) % numEnemies;
            enemyUpdater.setEnemySprite(currentEnemyNum, ref enemySprite, ref enemyManager, this.soundManager);
        }

        public void cycleEnemiesBackwards()
        {
            currentEnemyNum = (currentEnemyNum - 1 + numEnemies) % numEnemies;
            enemyUpdater.setEnemySprite(currentEnemyNum, ref enemySprite, ref enemyManager, this.soundManager);
        }

        public void GetDevRoom()
        {
            RoomChange.SwitchRoom("room1", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
            link = new Link(viewport.Width, viewport.Height, soundManager);
            foreach (IController controller in controllerList)
            {
                if (controller is KeyboardController keyboardController)
                {
                    keyboardController.UpdateLink(link);
                }
            }
        }

        public void SwitchToNextRoom() {
            RoomChange.SwitchToNextRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
        }

        public void SwitchToPreviousRoom() {
            RoomChange.SwitchToPreviousRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
        }
    }
}
