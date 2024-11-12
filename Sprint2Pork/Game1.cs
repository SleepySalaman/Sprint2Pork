using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint2Pork.Blocks;
using Sprint2Pork.Constants;
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

        //character, fb, enemy, gel, bat, goriya, wizard, stalfos, blocks, ItemsAndWeapons, ItemsAndWeaponsExpanded
        private List<Texture2D> allTextures;

        private SpriteFont font;

        private int[] spritePos = new int[2] { 0, 0 };
        private bool moving = false;
        private Texture2D backgroundTexture;
        private Texture2D hudTexture;
        private Texture2D roomTexture;
        private Texture2D nextRoomTexture;
        private Texture2D lifeTexture;
        private Texture2D pauseOverlayTexture;

        private Texture2D hitboxTexture;

        private double timeSinceLastSwitch = 0;
        private double timeSinceSwitchedEnemy = 0;
        private List<Block> blocks;
        private List<GroundItem> groundItems;
        private List<IEnemy> enemies;
        private List<EnemyManager> fireballManagers;
        private Vector2 enemyInitPos = new Vector2(GameConstants.ENEMY_INIT_X, GameConstants.ENEMY_INIT_Y);
        private Texture2D winStateTexture;
        private int currentBlockIndex = 0;
        private Vector2 blockPosition = new Vector2(200, 200);

        private List<GroundItem> items;
        private int currentItemIndex = 0;

        private EnemyManager enemyManager;
        private UpdateEnemySprite enemyUpdater;

        private int currentEnemyNum = 0;
        private int numEnemies = 12;

        private Link link;
        private HUD hud;
        private Inventory inventory;

        public Viewport viewport;

        public bool showHitboxes = false;
        public bool menu = false;
        private string currentRoom;
        private string nextRoom;
        private int lifeCount;
        private LinkHealth healthCount = new LinkHealth();
        private Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms;
        private Paused pausedScreen;
        // SFX
        private SoundManager soundManager;

        // Game States
        public Game1State gameState { get; private set; }
        private Game1StateManager gameStateManager;
        private float transitionDuration = GameConstants.TRANSITION_DURATION;
        private float transitionTimer = 0f;
        private Rectangle oldRoomRectangle;
        private Rectangle oldRoomRectangleSaved;
        private Rectangle nextRoomRectangle;
        private Rectangle nextRoomRectangleSaved;
        private Vector2 transitionDirection;
        private Texture2D startScreenTexture;

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

            gameState = Game1State.StartScreen; // Change initial state to StartScreen
            gameStateManager = new Game1StateManager(gameState);
        }


        private void LoadGroundItems()
        {
            // Load items with predefined frames
            GroundItemsController itemController = new GroundItemsController();
            items = itemController.createGroundItems();
        }

        protected override void Initialize()
        {
            inventory = new Inventory();
            InitializeHandler.BaseInitialize(ref viewport, graphics, ref link, ref controllerList, ref blocks, this, soundManager, inventory);
            hud = new HUD(inventory, font);
            pausedScreen = new Paused(inventory);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreenTexture = Content.Load<Texture2D>("StartScreen");

            pauseOverlayTexture = new Texture2D(GraphicsDevice, 1, 1);
            pauseOverlayTexture.SetData(new[] { new Color(0, 0, 0, 0.5f) });

            //Loading Sounds
            soundManager.LoadAllSounds(Content);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Content.Load<Song>("backgroundMusic"));

            InitializeHandler.LoadEnemyContent(ref spriteBatch, ref enemyUpdater, ref enemyManager,
                GraphicsDevice, (int)enemyInitPos.X, (int)enemyInitPos.Y, soundManager);

            LoadTextures.LoadAllTextures(allTextures, Content);

            font = Content.Load<SpriteFont>("File");
            // Loading the background/room
            backgroundTexture = Content.Load<Texture2D>("Room1");
            hudTexture = Content.Load<Texture2D>("ZeldaHUD");
            roomTexture = Content.Load<Texture2D>("Room1Alone");
            lifeTexture = Content.Load<Texture2D>("Zelda_Lives");
            textSprite = new TextSprite(200, GameConstants.TEXT_DISPLAY, font);

            hud = new HUD(inventory, font);
            pausedScreen = new Paused(inventory);
            GenerateBlocks.fillBlockList(blocks, allTextures[8], blockPosition);
            winStateTexture = Content.Load<Texture2D>("WinScreen");
            hitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            hitboxTexture.SetData(new Color[] { Color.Red });

            //lifeDestinationRect = new Rectangle((viewport.Width * 13) / 21, GameConstants.HUD_HEIGHT / 3, 50, 50);

            LoadRooms(allTextures[8], allTextures[9], allTextures[2]);
        }

        private void LoadRooms(Texture2D blockTexture, Texture2D groundItemTexture, Texture2D enemyTexture)
        {
            CSVLevelLoader.LoadObjectsFromCSV("room1.csv", blockTexture, groundItemTexture, enemyTexture, out var room1Blocks, out var room1Items, out var room1Enemies, out var fireballManagerRoom1, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room2.csv", blockTexture, groundItemTexture, enemyTexture, out var room2Blocks, out var room2Items, out var room2Enemies, out var fireballManagersRoom2, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room3.csv", blockTexture, groundItemTexture, enemyTexture, out var room3Blocks, out var room3Items, out var room3Enemies, out var fireballManagersRoom3, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room4.csv", blockTexture, groundItemTexture, enemyTexture, out var room4Blocks, out var room4Items, out var room4Enemies, out var fireballManagersRoom4, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room5.csv", blockTexture, groundItemTexture, enemyTexture, out var room5Blocks, out var room5Items, out var room5Enemies, out var fireballManagersRoom5, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room6.csv", blockTexture, groundItemTexture, enemyTexture, out var room6Blocks, out var room6Items, out var room6Enemies, out var fireballManagersRoom6, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room7.csv", blockTexture, groundItemTexture, enemyTexture, out var room7Blocks, out var room7Items, out var room7Enemies, out var fireballManagersRoom7, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room8.csv", blockTexture, groundItemTexture, enemyTexture, out var room8Blocks, out var room8Items, out var room8Enemies, out var fireballManagersRoom8, soundManager);
            CSVLevelLoader.LoadObjectsFromCSV("room9.csv", blockTexture, groundItemTexture, enemyTexture, out var room9Blocks, out var room9Items, out var room9Enemies, out var fireballManagersRoom9, soundManager);

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
            if (gameState == Game1State.StartScreen)
            {
                UpdateControllers(); // Only update controllers to check for start input
            }
            else if (gameState == Game1State.Playing)
            {
                int linkPreviousX = link.GetX();
                int linkPreviousY = link.GetY();

                timeSinceLastSwitch += gameTime.ElapsedGameTime.TotalSeconds;
                timeSinceSwitchedEnemy += gameTime.ElapsedGameTime.TotalSeconds;

                UpdateControllers();
                UpdateGroundItems();
                UpdateEnemies(gameTime);
                UpdateLink(linkPreviousX, linkPreviousY);

                CheckRoomChange(gameState);

                base.Update(gameTime);
            }
            else if (gameState == Game1State.Transitioning)
            {
                HandleTransition(gameTime);
            }
            else if (gameState == Game1State.Paused)
            {
                UpdateControllers();
            }
            else if (gameState == Game1State.GameOver)
            {
                UpdateControllers();
            }
        }

        private void CheckForKey()
        {
            List<GroundItem> items = rooms[currentRoom].Item2;
            foreach (var item in items)
            {
                if (item.GetType() == typeof(Key))
                {
                    // TODO: Troubleshoot why it only plays every other time?
                    soundManager.PlaySound("sfxKeyAppears");
                }
            }
        }
        private void HandleTransition(GameTime gameTime)
        {
            transitionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            int transitionConstant = (int)(Math.Abs(transitionDirection.X) * (transitionTimer * GameConstants.TRANSITION_SPEED)) + (int)(Math.Abs(transitionDirection.Y) * (transitionTimer * GameConstants.VERTICAL_TRANSITION_SPEED));

            oldRoomRectangle = new Rectangle(oldRoomRectangleSaved.X - ((int)transitionDirection.X * transitionConstant), oldRoomRectangle.Y - ((int)transitionDirection.Y * transitionConstant), oldRoomRectangle.Width, oldRoomRectangle.Height);
            nextRoomRectangle = new Rectangle(nextRoomRectangleSaved.X - ((int)transitionDirection.X * transitionConstant), nextRoomRectangle.Y - ((int)transitionDirection.Y * transitionConstant), nextRoomRectangle.Width, nextRoomRectangle.Height);

            if (transitionTimer >= transitionDuration || ((0 >= ((int)transitionDirection.X * nextRoomRectangle.X)) && (0 >= ((int)transitionDirection.Y * nextRoomRectangle.Y) - (int)transitionDirection.Y * GameConstants.ROOM_Y_OFFSET)))
            {
                transitionTimer = 0f;
                gameState = Game1State.Playing;
                roomTexture = nextRoomTexture;
                RoomChange.SwitchRoom(nextRoom, ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
                currentRoom = nextRoom;
                CheckForKey();
            }
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            EnemyUpdater.UpdateEnemies(link, enemies, blocks, fireballManagers);
            EnemyUpdater.UpdateFireballs(enemyManager, ref link, ref fireballManagers, gameTime, ref healthCount);
            if (!healthCount.IsLinkAlive())
            {
                GameOver();
            }
        }

        private void UpdateLink(int linkPreviousX, int linkPreviousY)
        {
            ISprite itemSprite = link.linkItem.SpriteGet();
            if (itemSprite != null)
            {
                link.linkItem.SpriteSet(itemSprite);
            }

            foreach (Enemy e in enemies)
            {
                if (Collision.Collides(e.GetRect(), link.GetRect()))
                {
                    link.BeDamaged();
                    healthCount.TakeDamage();
                    if (!healthCount.IsLinkAlive())
                    {
                        GameOver();
                    }
                }
            }

            link.actionState.Update();
            link.LinkSpriteUpdate();
            if (link.IsLinkUsingItem())
            {
                link.linkItem.Update(link);
                foreach (Enemy e in enemies)
                {
                    if (link.linkItem.Collides(e.GetRect()))
                    {
                        e.TakeDamage();
                    }
                }
            }

            HandleBlockCollision(linkPreviousX, linkPreviousY);

            if (!healthCount.IsLinkAlive()) {
                GameOver();
            }
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
                    if (item is Key)
                    {
                        soundManager.PlaySound("sfxItemReceived");
                    }
                    else if (item is Triangle)
                    {
                        GameOver();
                    }else if (item is Potion)
                    {
                        healthCount.HealFullHeart();
                    }
                    else if (item is Heart)
                    {
                        healthCount.HealHalfHeart();
                    }
                    else
                    {
                        soundManager.PlaySound("sfxItemObtained");
                    }
                    item.PerformAction();
                    itemsToRemove.Add(item);
                    link.CollectItem(item);
                }
            }

            foreach (var item in itemsToRemove)
            {
                groundItems.Remove(item);
            }
        }

        private void CheckRoomChange(Game1State gameState)
        {
            if (currentRoom == "room1" && link.GetX() > GraphicsDevice.Viewport.Width - GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room2";
                nextRoomTexture = Content.Load<Texture2D>("Room2Alone");
                link.SetX(GameConstants.ROOM_EDGE_BUFFER);

                transitionDirection = new Vector2(1, 0);
                SetRectangles();
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room1" && link.GetX() < GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room5";
                nextRoomTexture = Content.Load<Texture2D>("Room5");
                transitionDirection = new Vector2(-1, 0);
                SetRectangles();
                link.SetX(GraphicsDevice.Viewport.Width - 101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room2" && link.GetX() > GraphicsDevice.Viewport.Width - GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room4";
                nextRoomTexture = Content.Load<Texture2D>("Room4");
                link.SetX(GameConstants.ROOM_EDGE_BUFFER);

                transitionDirection = new Vector2(1, 0);
                SetRectangles();
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room5" && link.GetX() > GraphicsDevice.Viewport.Width - GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room1";
                nextRoomTexture = Content.Load<Texture2D>("Room1Alone");
                link.SetX(GameConstants.ROOM_EDGE_BUFFER);

                transitionDirection = new Vector2(1, 0);
                SetRectangles();
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room2" && link.GetX() < GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room1";
                nextRoomTexture = Content.Load<Texture2D>("Room1Alone");
                transitionDirection = new Vector2(-1, 0);
                SetRectangles();
                link.SetX(GraphicsDevice.Viewport.Width - 101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room4" && link.GetX() < GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room2";
                nextRoomTexture = Content.Load<Texture2D>("Room2Alone");
                transitionDirection = new Vector2(-1, 0);
                SetRectangles();
                link.SetX(GraphicsDevice.Viewport.Width - 101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room2" && link.GetY() < GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room3";
                nextRoomTexture = Content.Load<Texture2D>("Room3");
                transitionDirection = new Vector2(0, -1);
                SetRectangles();
                link.SetY(GraphicsDevice.Viewport.Height - 99);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room3" && link.GetY() > GraphicsDevice.Viewport.Height - GameConstants.ROOM_EDGE_THRESHOLD)
            {
                nextRoom = "room2";
                nextRoomTexture = Content.Load<Texture2D>("Room2Alone");
                transitionDirection = new Vector2(0, 1);
                SetRectangles();
                link.SetY(101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room5" && link.GetY() > GraphicsDevice.Viewport.Height - GameConstants.ROOM_EDGE_THRESHOLD)
            {
                nextRoom = "room6";
                nextRoomTexture = Content.Load<Texture2D>("Room6");
                transitionDirection = new Vector2(0, 1);
                SetRectangles();
                link.SetY(101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room6" && link.GetY() < GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room5";
                nextRoomTexture = Content.Load<Texture2D>("Room5");
                transitionDirection = new Vector2(0, -1);
                SetRectangles();
                link.SetY(GraphicsDevice.Viewport.Height - 70);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room6" && link.GetY() > GraphicsDevice.Viewport.Height - GameConstants.ROOM_EDGE_THRESHOLD)
            {
                nextRoom = "room7";
                nextRoomTexture = Content.Load<Texture2D>("Room7");
                transitionDirection = new Vector2(0, 1);
                SetRectangles();
                link.SetY(101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room7" && link.GetY() < GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room6";
                nextRoomTexture = Content.Load<Texture2D>("Room6");
                transitionDirection = new Vector2(0, -1);
                SetRectangles();
                link.SetY(GraphicsDevice.Viewport.Height - 99);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room5" && link.GetX() < GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room8";
                nextRoomTexture = Content.Load<Texture2D>("Room8");
                transitionDirection = new Vector2(-1, 0);
                SetRectangles();
                link.SetX(GraphicsDevice.Viewport.Width - 101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room8" && link.GetX() > GraphicsDevice.Viewport.Width - GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room5";
                nextRoomTexture = Content.Load<Texture2D>("Room5");
                link.SetX(GameConstants.ROOM_EDGE_BUFFER);
                transitionDirection = new Vector2(1, 0);
                SetRectangles();
                this.gameState = Game1State.Transitioning;
            }
            else if (currentRoom == "room8" && link.GetY() < GameConstants.ROOM_EDGE_BUFFER)
            {
                nextRoom = "room9";
                nextRoomTexture = Content.Load<Texture2D>("Room9");
                transitionDirection = new Vector2(0, -1);
                SetRectangles();
                link.SetY(GraphicsDevice.Viewport.Height - 99);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }

            else if (currentRoom == "room9" && link.GetY() > GraphicsDevice.Viewport.Height - GameConstants.ROOM_EDGE_THRESHOLD)
            {
                nextRoom = "room8";
                nextRoomTexture = Content.Load<Texture2D>("Room8");
                transitionDirection = new Vector2(0, 1);
                SetRectangles();
                link.SetY(101);
                currentRoom = nextRoom;
                this.gameState = Game1State.Transitioning;
            }
            hud.UpdateRoomNumber(GetCurrentRoomNumber());
        }

        private void SetRectangles()
        {
            oldRoomRectangle = new Rectangle(0, GameConstants.ROOM_Y_OFFSET, viewport.Width, viewport.Height - GameConstants.ROOM_Y_OFFSET);
            oldRoomRectangleSaved = oldRoomRectangle;
            nextRoomRectangle = new Rectangle(((int)transitionDirection.X * viewport.Width), GameConstants.ROOM_Y_OFFSET + ((int)transitionDirection.Y * viewport.Height), viewport.Width, viewport.Height - GameConstants.ROOM_Y_OFFSET);
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
            currentItem.destinationRect = new Rectangle(400, 200, GameConstants.ITEM_SPRITE_SIZE, GameConstants.ITEM_SPRITE_SIZE);
        }
        public void ResetGame()
        {
            currentBlockIndex = 0;
            currentItemIndex = 0;
            spritePos[0] = GameConstants.DEFAULT_SPRITE_POSITION;
            spritePos[1] = GameConstants.DEFAULT_SPRITE_POSITION;
            moving = false;

            enemyUpdater = new UpdateEnemySprite((int)enemyInitPos.X, (int)enemyInitPos.Y);

            soundManager = new SoundManager();
            soundManager.LoadAllSounds(Content);
            inventory.Reset();
            link = new Link(viewport.Width, viewport.Height, soundManager, inventory);

            currentRoom = "room2";
            (blocks, groundItems, enemies, fireballManagers) = rooms[currentRoom];
            gameState = Game1State.Playing;

            healthCount = new LinkHealth();

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

            GraphicsDevice.Clear(Color.Black);


            if (gameState == Game1State.StartScreen)
            {
                // Draw the start screen texture to fill the entire viewport
                spriteBatch.Draw(
                    startScreenTexture,
                    new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                    Color.White
                );
            }
            else if (gameState == Game1State.GameOver)
            {
                spriteBatch.Draw(winStateTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);
            }
            else
            {
                spriteBatch.Draw(hudTexture, new Rectangle(0, 0, viewport.Width, GameConstants.HUD_HEIGHT), Color.White);
                healthCount.DrawLives(spriteBatch, lifeTexture, viewport);

                if (gameState == Game1State.Playing)
                {
                    spriteBatch.Draw(roomTexture, new Rectangle(0, GameConstants.ROOM_Y_OFFSET, viewport.Width, viewport.Height - GameConstants.ROOM_Y_OFFSET), Color.White);
                    link.Draw(spriteBatch, allTextures[0], allTextures[10]);
                    Drawing.DrawGeneratedObjects(spriteBatch, blocks, groundItems, enemies, fireballManagers, allTextures, lifeTexture,
                        hitboxTexture, showHitboxes);
                    hud.Draw(spriteBatch);
                }
                else if (gameState == Game1State.Transitioning)
                {
                    spriteBatch.Draw(roomTexture, oldRoomRectangle, Color.White);
                    spriteBatch.Draw(nextRoomTexture, nextRoomRectangle, Color.White);
                    //hud.Draw(spriteBatch);
                }
                else if (gameState == Game1State.Paused)
                {
                    spriteBatch.Draw(roomTexture, new Rectangle(0, GameConstants.ROOM_Y_OFFSET, viewport.Width, viewport.Height - GameConstants.ROOM_Y_OFFSET), Color.White);
                    link.Draw(spriteBatch, allTextures[0], allTextures[10]);
                    Drawing.DrawGeneratedObjects(spriteBatch, blocks, groundItems, enemies, fireballManagers, allTextures, lifeTexture, hitboxTexture, showHitboxes);

                    spriteBatch.Draw(pauseOverlayTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Color(0, 0, 0, 400));

                    pausedScreen.DrawPausedScreen(spriteBatch, font, viewport, allTextures[9]);

                }
                else if (gameState == Game1State.GameOver)
                {
                    spriteBatch.DrawString(font, "Game Over", new Vector2(GameConstants.TEXT_DISPLAY, GameConstants.TEXT_DISPLAY), Color.Red);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void StartGame()
        {
            if (gameState == Game1State.StartScreen)
            {
                gameState = Game1State.Playing;
            }
            else
            {
                gameState = Game1State.StartScreen;
            }
        }

        public void GetDevRoom()
        {
            RoomChange.SwitchRoom("room1", ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
            link = new Link(viewport.Width, viewport.Height, soundManager, inventory);
            foreach (IController controller in controllerList)
            {
                if (controller is KeyboardController keyboardController)
                {
                    keyboardController.UpdateLink(link);
                }
            }
        }

        public void SwitchToNextRoom()
        {
            RoomChange.SwitchToNextRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
        }

        public void SwitchToPreviousRoom()
        {
            RoomChange.SwitchToPreviousRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
        }

        public void TogglePause()

        {
            if (gameState == Game1State.Playing)
            {
                gameState = Game1State.Paused;
            }
            else if (gameState == Game1State.Paused)
            {
                gameState = Game1State.Playing;
            }
        }

        public void GameOver()
        {
            gameState = Game1State.GameOver;
        }
        public void ToggleBackgroundMusic()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
            else
            {
                MediaPlayer.Resume();
            }
        }

        private int GetCurrentRoomNumber()
        {
            if (int.TryParse(currentRoom.Substring(4), out int roomNumber))
            {
                return roomNumber;
            }
            return -1;
        }
    }
}
