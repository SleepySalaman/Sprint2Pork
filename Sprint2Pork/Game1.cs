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
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace Sprint2Pork
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        private Minimap minimap;

        public bool IsFullscreen { get; set; }
        private SpriteBatch spriteBatch;
        private List<IController> controllerList;

        private ISprite textSprite;

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
        private Dictionary<string, Rectangle> itemSourceRects;

        public Viewport viewport;

        public bool showHitboxes = false;
        public bool menu = false;
        private string currentRoom;
        private string nextRoom;
        private int lifeCount;
        private LinkHealth healthCount = new LinkHealth();
        private Dictionary<string, (List<Block>, List<GroundItem>, List<IEnemy>, List<EnemyManager>)> rooms;
        private Paused pausedScreen;
        private SoundManager soundManager;

        public Game1State gameState { get; private set; }
        private Game1StateManager gameStateManager;
        private float transitionDuration = GameConstants.TRANSITION_DURATION;
        private float transitionTimer = 0f;
        private Rectangle oldRoomRectangle;
        private Rectangle oldRoomRectangleSaved;
        private Rectangle nextRoomRectangle;
        private Rectangle nextRoomRectangleSaved;
        private Rectangle roomBoundingBox = new Rectangle(70, 110, 660, 800);
        private Vector2 transitionDirection;
        private Texture2D startScreenTexture;

        private RoomManager roomManager;

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

            gameState = Game1State.StartScreen;
            gameStateManager = new Game1StateManager(gameState);


        }

        private void LoadGroundItems()
        {
            GroundItemsController itemController = new GroundItemsController();
            items = itemController.createGroundItems();
        }

        protected override void Initialize()
        {
            inventory = new Inventory();
            InitializeHandler.BaseInitialize(ref viewport, graphics, ref link, ref controllerList, ref blocks, this, soundManager, inventory);
            hud = new HUD(inventory, font, link);
            pausedScreen = new Paused(inventory);
            minimap = new Minimap(GraphicsDevice, link);
            roomManager = new RoomManager(GraphicsDevice);
            roomManager.InitializeRooms(Content);
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
            roomTexture = Content.Load<Texture2D>("Room1");
            lifeTexture = Content.Load<Texture2D>("Zelda_Lives");
            textSprite = new TextSprite(200, GameConstants.TEXT_DISPLAY, font);

            hud = new HUD(inventory, font, link);
            pausedScreen = new Paused(inventory);
            GenerateBlocks.fillBlockList(blocks, allTextures[8], blockPosition);
            winStateTexture = Content.Load<Texture2D>("WinScreen");
            hitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            hitboxTexture.SetData(new Color[] { Color.Red });

            RoomLoader.LoadRooms(allTextures[8], allTextures[9], allTextures[2], ref blocks, ref groundItems, ref enemies,
                ref fireballManagers, ref rooms, ref soundManager, ref currentRoom);
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
            else if (gameState == Game1State.Inventory)
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
            EnemyUpdater.UpdateEnemies(link, enemies, blocks, fireballManagers, healthCount);
            EnemyUpdater.UpdateFireballs(enemyManager, ref link, ref fireballManagers, gameTime, ref healthCount);
            if (!healthCount.IsLinkAlive())
            {
                ResetGame();
            }
        }

        private void UpdateLink(int linkPreviousX, int linkPreviousY)
        {
            ISprite itemSprite = link.linkItem.SpriteGet();
            if (itemSprite != null) {
                link.linkItem.SpriteSet(itemSprite);
            }

            link.actionState.Update();
            link.LinkSpriteUpdate();
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
                if (link.linkItem.Collides(block.getBoundingBox())) {
                    link.endItem();
                }
            }
            if (link.IsLinkUsingItem()) {
                if (Collision.CollidesWithOutside(link.linkItem.SpriteGet().GetRect(), roomBoundingBox)) {
                    link.endItem();
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
            nextRoom = roomManager.GetNextRoom(currentRoom, link);
            if (nextRoom != "none")
            {
                nextRoomTexture = roomManager.GetNextRoomTexture(nextRoom);
                transitionDirection = roomManager.GetDirection(link);
                roomManager.PlaceLink(link, GraphicsDevice);
                SetRectangles();
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
            hud.SubscribeToLinkEvents(link);

            currentRoom = "room2";
            (blocks, groundItems, enemies, fireballManagers) = rooms[currentRoom];
            gameState = Game1State.Playing;

            healthCount = new LinkHealth();

            RoomLoader.LoadRooms(allTextures[8], allTextures[9], allTextures[2], ref blocks, ref groundItems, ref enemies,
                ref fireballManagers, ref rooms, ref soundManager, ref currentRoom);

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
            if (gameState == Game1State.StartScreen) {
                DrawStartScreen();
            } else if (gameState == Game1State.GameOver) {
                DrawGameOverScreen();
            } else {
                spriteBatch.Draw(hudTexture, new Rectangle(0, 0, viewport.Width, GameConstants.HUD_HEIGHT), Color.White);
                healthCount.DrawLives(spriteBatch, lifeTexture, viewport);

                if (gameState == Game1State.Playing) {
                    DrawPlayingScreen();

                } else if (gameState == Game1State.Transitioning) {
                    DrawTransitioningScreen();
                } else if (gameState == Game1State.Paused) {
                    DrawPausedScreen();
                } else if (gameState == Game1State.Inventory) {
                    DrawInventoryScreen();
                } else if (gameState == Game1State.GameOver) {
                    spriteBatch.DrawString(font, "Game Over", new Vector2(GameConstants.TEXT_DISPLAY, GameConstants.TEXT_DISPLAY), Color.Red);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawInventoryScreen()
        {
            // Draw the HUD and minimap
            hud.Draw(spriteBatch, allTextures[9]);
            minimap.Draw(spriteBatch, blocks, groundItems, enemies, new Rectangle(120, 15, 140, 5), 0.15f);

            // Draw the inventory items
            DrawInventoryItems();
        }
        private void InitializeItemSourceRects()
        {
            itemSourceRects = new Dictionary<string, Rectangle>
            {
                { "GroundBomb", new Rectangle(136, 0, 8, 16) },
                { "Sword", new Rectangle(104, 16, 6, 16) },
                { "Arrow", new Rectangle(152, 16, 8, 16) },
                { "Boomerang", new Rectangle(128, 0, 8, 16) },
                { "WoodArrow", new Rectangle(152, 0, 8, 16) },
                { "BlueBoomer", new Rectangle(128, 16, 8, 16) },
                { "Fire", new Rectangle(224, 0, 8, 16) },
            };
        }
        private void DrawInventoryItems(float scale = 4.0f)
        {
            int startX = GameConstants.INVENTORY_START_X;
            int startY = GameConstants.INVENTORY_START_Y;
            int itemSize = GameConstants.INVENTORY_ITEM_SIZE;
            int padding = GameConstants.INVENTORY_PADDING;
            int itemsPerRow = GameConstants.INVENTORY_ITEMS_PER_ROW;
            InitializeItemSourceRects();

            for (int i = 0; i < itemSourceRects.Count; i++)
            {
                var item = itemSourceRects.ElementAt(i);
                Rectangle sourceRect = item.Value;
                int row = i / itemsPerRow;
                int col = i % itemsPerRow;
                Vector2 position = new Vector2(startX + (itemSize + padding) * col, startY + (itemSize + padding) * row);

                spriteBatch.Draw(allTextures[9], position, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                // If the item is the one in SlotB, draw a selection box around it
                if (item.Key == link.SlotB)
                {
                    Rectangle selectionBox = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRect.Width * scale), (int)(sourceRect.Height * scale));
                    DrawSelectionBox(selectionBox);
                }
            }
            minimap.Draw(spriteBatch, blocks, groundItems, enemies, new Rectangle(50, 200, 200, 200), 0.3f);
        }

        private void DrawSelectionBox(Rectangle rectangle)
        {
            int thickness = 1;
            Color color = Color.Maroon;

            // Draw top line
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width+GameConstants.INVENTORY_RECTANGLE_ADJUSTMENT_SIZE, thickness), color);
            // Draw left line
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
            // Draw right line
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Right+ GameConstants.INVENTORY_RECTANGLE_ADJUSTMENT_SIZE - thickness, rectangle.Top, thickness, rectangle.Height), color);
            // Draw bottom line
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width+ GameConstants.INVENTORY_RECTANGLE_ADJUSTMENT_SIZE, thickness), color);
        }

        private void DrawGameOverScreen()
        {
            spriteBatch.Draw(winStateTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);
        }

        private void DrawStartScreen()
        {
            // Draw the start screen texture to fill the entire viewport
            spriteBatch.Draw(
                startScreenTexture,
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                Color.White
            );
        }

        private void DrawPlayingScreen()
        {
            spriteBatch.Draw(roomTexture, new Rectangle(0, GameConstants.ROOM_Y_OFFSET, viewport.Width, viewport.Height - GameConstants.ROOM_Y_OFFSET), Color.White);
            link.Draw(spriteBatch, allTextures[0], allTextures[10]);
            Drawing.DrawGeneratedObjects(spriteBatch, blocks, groundItems, enemies, fireballManagers, allTextures, lifeTexture,
                hitboxTexture, showHitboxes);
            hud.Draw(spriteBatch, allTextures[9]);
            minimap.Draw(spriteBatch, blocks, groundItems, enemies, new Rectangle(120, 15, 140, 5), 0.15f);
        }

        private void DrawTransitioningScreen()
        {
            spriteBatch.Draw(roomTexture, oldRoomRectangle, Color.White);
            spriteBatch.Draw(nextRoomTexture, nextRoomRectangle, Color.White);
        }

        private void DrawPausedScreen()
        {
            spriteBatch.Draw(roomTexture, new Rectangle(0, GameConstants.ROOM_Y_OFFSET, viewport.Width, viewport.Height - GameConstants.ROOM_Y_OFFSET), Color.White);
            link.Draw(spriteBatch, allTextures[0], allTextures[10]);
            Drawing.DrawGeneratedObjects(spriteBatch, blocks, groundItems, enemies, fireballManagers, allTextures, lifeTexture, hitboxTexture, showHitboxes);

            spriteBatch.Draw(pauseOverlayTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Color(0, 0, 0, 400));

            pausedScreen.DrawPausedScreen(spriteBatch, font, viewport, allTextures[9]);
            minimap.Draw(spriteBatch, blocks, groundItems, enemies,
                        new Rectangle(50, 200, 200, 200), 0.3f);
        }

        public void StartGame()
        {
            if (gameState == Game1State.StartScreen){
                gameState = Game1State.Playing;
            } else {
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
            roomTexture = roomManager.GetNextRoomTexture(currentRoom);
        }

        public void SwitchToPreviousRoom()
        {
            RoomChange.SwitchToPreviousRoom(ref currentRoom, ref blocks, ref groundItems, ref enemies, ref fireballManagers, rooms);
            roomTexture = roomManager.GetNextRoomTexture(currentRoom);
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

        public void ToggleInventory()

        {
            if (gameState == Game1State.Playing)
            {
                gameState = Game1State.Inventory;
            }
            else if (gameState == Game1State.Inventory)
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
