using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint2Pork.Blocks;
using Sprint2Pork.Constants;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Essentials;
using Sprint2Pork.Items;
using Sprint2Pork.Managers;
using Sprint2Pork.rooms;
using Sprint2Pork.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Sprint2Pork
{
    public class Game1 : Game
    {
        // Graphics and Rendering
        public GraphicsDeviceManager graphics;
        public Viewport viewport;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private UpdateManager updateManager;

        // Textures
        private Texture2D backgroundTexture;
        private Texture2D hudTexture;
        public Texture2D roomTexture;
        private Texture2D nextRoomTexture;
        private Texture2D lifeTexture;
        private Texture2D pauseOverlayTexture;
        private Texture2D hitboxTexture;
        private Texture2D winStateTexture;
        private Texture2D startScreenTexture;
        public List<Texture2D> allTextures;

        // Game State and Management
        public Game1State gameState { get; private set; }
        public Game1StateManager gameStateManager;
        public string currentRoom;
        private string nextRoom;

        // UI and HUD Components
        public HUD hud;
        public Minimap minimap;
        private Paused pausedScreen;
        private ISprite textSprite;
        public Inventory inventory;

        // Player and Character Related
        public Link link;
        public LinkHealth healthCount = new LinkHealth();
        public int[] spritePos = new int[2] { 0, 0 };

        // Enemy and Enemy Management
        public List<IEnemy> enemies;
        public List<EnemyManager> fireballManagers;
        private EnemyManager enemyManager;
        public UpdateEnemySprite enemyUpdater;
        public Vector2 enemyInitPos = new Vector2(GameConstants.ENEMY_INIT_X, GameConstants.ENEMY_INIT_Y);
        public float enemyStopTimer;
        public bool isEnemyStopActive;

        // Game World and Room Management
        public RoomManager roomManager;
        public Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms;
        public List<Block> blocks;
        public List<GroundItem> groundItems;
        private Vector2 blockPosition = new Vector2(200, 200);
        private Rectangle roomBoundingBox = new Rectangle(50, 110, 660, 850);
        private GameStateManager stateManager;

        //text
        public static string textToDisplay = "";
        public static float textDisplayTimer = 0f;
        public static float textDelayTimer = 0.5f; // Half second delay before text appears
        public static bool isTextDelaying = false;

        // Transition and Room-related Variables
        private float transitionDuration = GameConstants.TRANSITION_DURATION;
        private float transitionTimer = 0f;
        private Rectangle oldRoomRectangle;
        private Rectangle oldRoomRectangleSaved;
        private Rectangle nextRoomRectangle;
        private Rectangle nextRoomRectangleSaved;
        private Vector2 transitionDirection;

        // Audio
        public SoundManager soundManager;

        // Input and Control
        public List<IController> controllerList;

        // Toggles and Flags
        public bool IsFullscreen { get; set; }
        public bool showHitboxes = false;
        public bool menu = false;

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

            rooms = new Dictionary<string, (List<Block> blocks, List<GroundItem> groundItems,
                List<IEnemy> enemies, List<EnemyManager> fireballs)>();

            gameState = Game1State.StartScreen;
            gameStateManager = new Game1StateManager(gameState);


        }

        protected override void Initialize()
        {
            inventory = new Inventory();
            InitializeHandler.BaseInitialize(ref viewport, graphics, ref link, ref controllerList, ref blocks, this, soundManager, inventory);
            updateManager = new UpdateManager(this, link, healthCount, controllerList, soundManager);
            hud = new HUD(inventory, font, link);
            pausedScreen = new Paused(inventory);
            minimap = new Minimap(GraphicsDevice, link);
            roomManager = new RoomManager(GraphicsDevice);
            stateManager = new GameStateManager(this, link, soundManager, inventory, healthCount, controllerList, minimap, hud, GraphicsDevice);
            roomManager.InitializeRooms(Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadContentTextures();
            LoadSounds();
            InitializeGameComponents();
            LoadRooms();
        }

        private void LoadContentTextures()
        {
            startScreenTexture = Content.Load<Texture2D>("StartScreen");
            pauseOverlayTexture = new Texture2D(GraphicsDevice, 1, 1);
            pauseOverlayTexture.SetData(new[] { new Color(0, 0, 0, 0.5f) });

            font = Content.Load<SpriteFont>("File");
            backgroundTexture = Content.Load<Texture2D>("Room1");
            hudTexture = Content.Load<Texture2D>("ZeldaHUD");
            roomTexture = Content.Load<Texture2D>("Room1");
            lifeTexture = Content.Load<Texture2D>("Zelda_Lives");
            winStateTexture = Content.Load<Texture2D>("WinScreen");
            hitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            hitboxTexture.SetData(new Color[] { Color.Red });

            LoadTextures.LoadAllTextures(allTextures, Content);
        }

        private void LoadSounds()
        {
            soundManager.LoadAllSounds(Content);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Content.Load<Song>("backgroundMusic"));
        }

        public void InitializeGameComponents()
        {
            InitializeHandler.LoadEnemyContent(ref spriteBatch, ref enemyUpdater, ref enemyManager,
                GraphicsDevice, (int)enemyInitPos.X, (int)enemyInitPos.Y, soundManager);

            textSprite = new TextSprite(200, GameConstants.TEXT_DISPLAY, font);
            hud = new HUD(inventory, font, link);
            pausedScreen = new Paused(inventory);
        }

        private void LoadRooms()
        {
            RoomLoader.LoadRooms(allTextures[8], allTextures[9], allTextures[2], ref blocks, ref groundItems, ref enemies,
                ref fireballManagers, ref rooms, ref soundManager, ref currentRoom);
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameState == Game1State.StartScreen)
            {
                updateManager.UpdateControllers();
            }
            else if (gameState == Game1State.Playing)
            {
                int linkPreviousX = link.GetX();
                int linkPreviousY = link.GetY();

                updateManager.UpdateControllers();
                updateManager.UpdateGroundItems(groundItems);
                updateManager.UpdateEnemyStopClock(this, enemyStopTimer, isEnemyStopActive, gameTime);
                updateManager.UpdateEnemies(enemies, blocks, fireballManagers, gameTime, enemyManager, enemyStopTimer, isEnemyStopActive);
                updateManager.UpdateLink(linkPreviousX, linkPreviousY, gameTime, blocks, roomBoundingBox);

                CheckRoomChange(gameState);

                base.Update(gameTime);
            }
            else if (gameState == Game1State.Transitioning)
            {
                HandleTransition(gameTime);
            }
            else if (gameState == Game1State.Paused || gameState == Game1State.GameOver || gameState == Game1State.Inventory)
            {
                updateManager.UpdateControllers();
            }
        }

        private void CheckForKey()
        {
            List<GroundItem> items = rooms[currentRoom].Item2;
            foreach (var item in items)
            {
                if (item.GetType() == typeof(Key))
                {
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

        private void CheckRoomChange(Game1State gameState)
        {
            RoomChange.CheckRoomChange(gameState, ref currentRoom, ref nextRoom, ref nextRoomTexture, ref transitionDirection, roomManager, link, GraphicsDevice, hud, SetRectangles, state => this.gameState = state, GetCurrentRoomNumber, inventory);
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
        public void SetGameState(Game1State newState)
        {
            gameState = newState;
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);

            switch (gameState)
            {
                case Game1State.StartScreen:
                    spriteBatch.Draw(startScreenTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    break;
                case Game1State.GameOver:
                    int margin = 50;
                    spriteBatch.Draw(hitboxTexture,
                        new Rectangle(margin,
                                     GameConstants.HUD_HEIGHT + margin,
                                     viewport.Width - (margin * 2),
                                     viewport.Height - GameConstants.HUD_HEIGHT - (margin * 2)),
                        Color.Black);

                    Vector2 textPosition = new Vector2(viewport.Width / 2 - font.MeasureString("GAME OVER").X / 2,
                                                     viewport.Height / 2 - font.MeasureString("GAME OVER").Y / 2);
                    spriteBatch.DrawString(font, "Game Over\n\nPress R to Restart", textPosition, Color.Red);
                    break;
                default:
                    DrawHUD();
                    Drawing.DrawGameState(gameState, spriteBatch, font, winStateTexture, viewport, this);
                    break;
            }
            if (textDisplayTimer > 0)
            {
                if (isTextDelaying)
                {
                    textDelayTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (textDelayTimer <= 0)
                    {
                        isTextDelaying = false;
                    }
                }
                else
                {
                    Vector2 textPosition = new Vector2(viewport.Width / 2 - font.MeasureString(textToDisplay).X / 2,
                                                     viewport.Height / 2);
                    spriteBatch.DrawString(font, textToDisplay, textPosition, Color.White);
                    textDisplayTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawHUD()
        {
            spriteBatch.Draw(hudTexture, new Rectangle(0, 0, viewport.Width, GameConstants.HUD_HEIGHT), Color.White);
            healthCount.DrawLives(spriteBatch, lifeTexture, viewport);
        }

        public void DrawInventoryScreen()
        {
            Drawing.DrawInventoryScreen(spriteBatch, hud, minimap, blocks, groundItems, enemies, font, viewport, allTextures[9], this);
        }

        public void DrawPlayingScreen()
        {
            spriteBatch.Draw(roomTexture, new Rectangle(0, GameConstants.ROOM_Y_OFFSET, viewport.Width, viewport.Height - GameConstants.ROOM_Y_OFFSET), Color.White);
            link.Draw(spriteBatch, allTextures[0], allTextures[10]);
            Drawing.DrawGeneratedObjects(spriteBatch, blocks, groundItems, enemies, fireballManagers, allTextures, lifeTexture,
                hitboxTexture, showHitboxes);
            hud.Draw(spriteBatch, allTextures[9]);
            minimap.Draw(spriteBatch, blocks, groundItems, enemies, new Rectangle(120, 15, 140, 5), 0.15f);
        }

        public void DrawTransitioningScreen()
        {
            spriteBatch.Draw(roomTexture, oldRoomRectangle, Color.White);
            spriteBatch.Draw(nextRoomTexture, nextRoomRectangle, Color.White);
        }

        public void GetDevRoom()
        {
            currentRoom = "room1";
            roomTexture = roomManager.GetNextRoomTexture(currentRoom);
            roomManager.GetDevRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms, ref link, viewport, soundManager, controllerList);
        }

        public void SwitchToNextRoom()
        {
            RoomChange.SwitchToNextRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
            roomTexture = roomManager.GetNextRoomTexture(currentRoom);
        }

        public void SwitchToPreviousRoom()
        {
            RoomChange.SwitchToPreviousRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
            roomTexture = roomManager.GetNextRoomTexture(currentRoom);
        }

        public void GameOver()
        {
            stateManager.GameOver();
        }

        public void TogglePause()
        {
            stateManager.TogglePause();
        }

        public void ToggleBackgroundMusic()
        {
            stateManager.ToggleBackgroundMusic();
        }

        public void ResetGame()
        {
            stateManager.ResetGame();
        }

        public void StartGame()
        {
            stateManager.StartGame();
        }

        private int GetCurrentRoomNumber()
        {
            return roomManager.GetCurrentRoomNumber(currentRoom);
        }

        public void InitEnemyStopTimer()
        {
            enemyStopTimer = GameConstants.ENEMY_FREEZE_TIMER;
            isEnemyStopActive = true;
        }

        public void ResetEnemyStopTimer()
        {
            enemyStopTimer = 0.0f;
            isEnemyStopActive = false;
        }
    }
}
