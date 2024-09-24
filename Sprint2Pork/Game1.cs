using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2Pork.Blocks;
using Sprint2Pork.Enemies;
using Sprint2Pork.Enemies.Aquamentus;
using Sprint2Pork.Enemies.Dodongo;
using Sprint2Pork.Enemies.Digdogger;
using Sprint2Pork.Enemies.Manhandla;
using Sprint2Pork.Enemies.Patra_Ganon;
using Sprint2Pork.Enemies.Gleeok;
using Sprint2Pork.Enemies.Gohma;
using Sprint2Pork.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace Sprint2Pork
{   
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<IController> controllerList;

        private ISprite characterSprite;
        private ISprite textSprite;

        private IEnemy enemySprite;

        private Texture2D characterTexture;
        private Texture2D linkTexture;
        private Texture2D enemyTexture;

        private Texture2D linkMovingTexture;
        private Texture2D linkAttackingTexture;

        private SpriteFont font;

        private int[] spritePos;
        private bool moving;
        private double switchCooldown = 0.1;  // 0.5 seconds cooldown between switches
        private double timeSinceLastSwitch = 0;

        // Block-related variables
        private Texture2D blockTexture;
        private List<Block> blocks;
        private int currentBlockIndex;
        private Vector2 blockPosition;  // Constant position for all blocks

        private List<Item> items;
        private int currentItemIndex;
        private Texture2D itemTexture;
        private KeyboardState previousState;

        public enum Direction { Left, Right };
        public enum PlayerSpriteList { NonMovingNonAnimatedPlayer, NonMovingAnimatedPlayer, MovingNonAnimatedPlayer, MovingAnimatedPlayer };
        public enum EnemyList { Aquamentus, Dodongo, Manhandla, Gleeok, Digdogger, Gohma, Ganon };

        private EnemyList currentEnemy;
        private PlayerSpriteList playerMode;
        private ISprite staticSprite;
        private ISprite animatedSprite;
        private ISprite currentSprite;
        private List<Rectangle> sourceRects;

        private ILink LinkMovingSprite;
        private ILink LinkAttackingSprite;

        public Viewport viewport;

        // Link
        private Link link;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            controllerList = new List<IController>();
            controllerList.Add(new KeyboardController(this));
            controllerList.Add(new MouseController(this));
            spritePos = new int[2];
            spritePos[0] = 50;
            spritePos[1] = 50;
            //spriteMode = 1;
            playerMode = PlayerSpriteList.NonMovingNonAnimatedPlayer;
            currentEnemy = EnemyList.Aquamentus;
            moving = false;

            blocks = new List<Block>();
            //adds item to list
            items = new List<Item> {
                new Item(300, 100, new List<Rectangle> { new Rectangle(72, 0, 8, 16), new Rectangle(72, 16, 8, 16) }),
                new Item(300, 100, new List<Rectangle> { new Rectangle(80, 0, 8, 16), new Rectangle(80, 16, 8, 16) }),
                new Item(300, 100, new List<Rectangle> { new Rectangle(88, 0, 8, 16), new Rectangle(88, 16, 8, 16) }),
                new Item(300, 100, new List<Rectangle> { new Rectangle(24, 0, 16, 16), new Rectangle(24, 0, 16, 16) })
            };

            currentBlockIndex = 0;
            currentItemIndex = 0;

            blockPosition = new Vector2(200, 200);  // Constant position for block

        }

        protected override void Initialize()
        {
            previousState = Keyboard.GetState();

            viewport = graphics.GraphicsDevice.Viewport;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            characterSprite = new NonMovingNonAnimatedSprite(spritePos[0], spritePos[1], new Rectangle(80, 0, 16, 16), false);
            enemySprite = new DodongoIdle();
            staticSprite = new NonMovingNonAnimatedSprite(spritePos[0], spritePos[1], new Rectangle(80, 0, 16, 16), false);
            animatedSprite = new MovingAnimatedSprite(spritePos[0], spritePos[1], new List<Rectangle>() { new Rectangle(84, 0, 16, 16) }, false, 30);
            currentSprite = staticSprite;
            characterTexture = Content.Load<Texture2D>("NES - The Legend of Zelda - Link");
            enemyTexture = Content.Load<Texture2D>("zeldaenemies");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            blockTexture = Content.Load<Texture2D>("blocks");

            // Create blocks at different positions
            // Define blocks using specific tiles from the sprite sheet
            // Define blocks using different source rectangles (tiles) but same position
            // Create blocks using a loop instead of hardcoding each one
            int numberOfBlocks = 10;
            for (int i = 0; i < numberOfBlocks; i++)
            {
                // Each block uses a tile that is 16x16 pixels in size, and they are placed horizontally on the texture
                blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * i, 0, 16, 16)));
            }
            font = Content.Load<SpriteFont>("File");
            textSprite = new TextSprite(200, 100, font);

            sourceRects = new List<Rectangle>();
            sourceRects.Add(new Rectangle(200, 120, 30, 35));
            sourceRects.Add(new Rectangle(230, 120, 30, 35));
            sourceRects.Add(new Rectangle(255, 120, 30, 35));

            itemTexture = Content.Load<Texture2D>("items_and_weapons");

            //Link
            link = new Link(viewport.Width, viewport.Height);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            // Quit the game if 'Q' is pressed
            if (state.IsKeyDown(Keys.Q))
            {
                Exit();
            }
            // Reset the game if 'R' is pressed
            if (state.IsKeyDown(Keys.R))
            {
                ResetGame();
            }
            // Existing code for quitting with Escape or Mouse click
            if (state.IsKeyDown(Keys.Escape) || state.IsKeyDown(Keys.D0) || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Exit();
            }
            foreach (IController c in controllerList)
            {
                c.Update();
            }

            bool isMoving = false;
            Vector2 newPosition = new Vector2(spritePos[0], spritePos[1]);

            //// Handle WASD movement
            //if (state.IsKeyDown(Keys.W))
            //{
            //    // Move up
            //    newPosition.Y -= 5;
            //    isMoving = true;
            //}
            //if (state.IsKeyDown(Keys.S))
            //{
            //    // Move down
            //    newPosition.Y += 5;
            //}
            //if (state.IsKeyDown(Keys.A))
            //{
            //    // Move left
            //    newPosition.X -= 5;
            //    isMoving = true;
            //}
            //if (state.IsKeyDown(Keys.D))
            //{
            //    // Move right
            //    newPosition.X += 5;
            //    isMoving = true;
            //}

            // Switch between static and animated sprites
            currentSprite = isMoving ? animatedSprite : staticSprite;

            // Update the current sprite
            currentSprite.Update(spritePos[0], spritePos[1]);
            enemySprite.Update();

            // Update the current sprite only if the position has changed
            if (newPosition != new Vector2(spritePos[0], spritePos[1])) {
                spritePos[0] = (int)newPosition.X;
                spritePos[1] = (int)newPosition.Y;
                currentSprite.Update(spritePos[0], spritePos[1]);
            }

            // Existing code for quitting with Escape or Mouse click
            if (state.IsKeyDown(Keys.Escape) || state.IsKeyDown(Keys.D0) || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Exit();
            }

            foreach (IController c in controllerList)
            {
                c.Update();
            }

            // Accumulate the elapsed time for block switching cooldown
            timeSinceLastSwitch += gameTime.ElapsedGameTime.TotalSeconds;

            // Switch to the previous block with 'T' and cooldown check
            if (state.IsKeyDown(Keys.T) && timeSinceLastSwitch >= switchCooldown && !previousState.IsKeyDown(Keys.T))
            {
                currentBlockIndex = (currentBlockIndex - 1 + blocks.Count) % blocks.Count;
                timeSinceLastSwitch = 0;
            }

            // Switch to the next block with 'Y' and cooldown check
            if (state.IsKeyDown(Keys.Y) && timeSinceLastSwitch >= switchCooldown && !previousState.IsKeyDown(Keys.Y))
            {
                currentBlockIndex = (currentBlockIndex + 1) % blocks.Count;
                timeSinceLastSwitch = 0;
            }

            // Handle item cycling
            if (state.IsKeyDown(Keys.U) && !previousState.IsKeyDown(Keys.U))
            {
                // Store the current position
                var currentPosition = items[currentItemIndex].destinationRect.Location;
                currentItemIndex = (currentItemIndex - 1 + items.Count) % items.Count;
                // Set the new current item's position to the stored position
                items[currentItemIndex].destinationRect.Location = currentPosition;
            }

            // Cycle to the next item with 'I'
            if (state.IsKeyDown(Keys.I) && !previousState.IsKeyDown(Keys.I))
            {
                // Store the current position
                var currentPosition = items[currentItemIndex].destinationRect.Location;
                currentItemIndex = (currentItemIndex + 1) % items.Count;
                // Set the new current item's position to the stored position
                items[currentItemIndex].destinationRect.Location = currentPosition;
            }

            previousState = state;

            // Link


            bool moving = false;
            // Handle WASD movement
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                // Move up
                moving = true;
                link.LookUp();
                //link.Move();
            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                // Move down
                moving = true;
                link.LookDown();
                //link.Move();
            }
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                // Move left
                moving = true;
                link.LookLeft();
                //link.Move();
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                // Move right
                moving = true;
                link.LookRight();
                //link.Move();
            }

            if (state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.S) && state.IsKeyUp(Keys.D) && state.IsKeyUp(Keys.Up) && state.IsKeyUp(Keys.Left) && state.IsKeyUp(Keys.Right) && state.IsKeyUp(Keys.Down))
            {
                moving = false;
                link.BeIdle();
            }
            else
            {
                moving = true;
                link.BeMoving();
            }
            //link.directionState.Update();
            link.actionState.Update();
            link.linkSprite.Update(link.x, link.y);



            // Update the current item
            items[currentItemIndex].Update(items[currentItemIndex].destinationRect.X, items[currentItemIndex].destinationRect.Y);


            base.Update(gameTime);
        }

        private void ResetGame()
        {
            // Reset block index to the first block
            currentBlockIndex = 0;
            currentItemIndex = 0;

            // Reset character position
            spritePos[0] = 50;
            spritePos[1] = 50;

            // Reset other necessary variables
            moving = false;
            timeSinceLastSwitch = 0;

            // Reset the character sprite to its initial mode (NonMovingNonAnimatedSprite)
            setMode(PlayerSpriteList.NonMovingNonAnimatedPlayer);
            enemySprite = new AquamentusNotAttacking();
            link = new Link(viewport.Width, viewport.Height);
        }


        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            enemySprite.Draw(spriteBatch, enemyTexture);
            textSprite.Draw(spriteBatch, characterTexture);

            // Existing code for quitting with Escape or Mouse click
            // Draw the current block
            blocks[currentBlockIndex].Draw(spriteBatch);

            // Draw the current item
            items[currentItemIndex].Draw(spriteBatch, itemTexture);

            link.Draw(spriteBatch, characterTexture);

            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void setMode(PlayerSpriteList spriteList){
            if (playerMode != spriteList){
                playerMode = spriteList;
                switch (spriteList){
                    case PlayerSpriteList.NonMovingNonAnimatedPlayer:
                        characterSprite = new NonMovingNonAnimatedSprite(spritePos[0], spritePos[1], new Rectangle(200, 120, 30, 35), false);
                        moving = false;
                        break;
                    case PlayerSpriteList.NonMovingAnimatedPlayer:
                        characterSprite = new NonMovingAnimatedSprite(spritePos[0], spritePos[1], sourceRects);
                        moving = false;
                        break;
                    case PlayerSpriteList.MovingNonAnimatedPlayer:
                        characterSprite = new MovingNonAnimatedSprite(spritePos[0], spritePos[1], new Rectangle(84, 0, 16, 16));
                        moving = true;
                        break;
                    case PlayerSpriteList.MovingAnimatedPlayer:
                        characterSprite = new MovingAnimatedSprite(spritePos[0], spritePos[1], new List<Rectangle>() { new Rectangle(84, 0, 16, 16) }, false, 30);
                        moving = true;
                        break;
                }
            }
        }

        public void cycleEnemies(EnemyList enemy) {
            switch (enemy) {
                case EnemyList.Aquamentus:
                    enemy = EnemyList.Dodongo;
                    break;
                case EnemyList.Dodongo:
                    enemy = EnemyList.Manhandla;
                    break;
                case EnemyList.Manhandla:
                    enemy = EnemyList.Gleeok;
                    break;
                case EnemyList.Gleeok:
                    enemy = EnemyList.Digdogger;
                    break;
                case EnemyList.Digdogger:
                    enemy = EnemyList.Gohma;
                    break;
                case EnemyList.Gohma:
                    enemy = EnemyList.Ganon;
                    break;
                case EnemyList.Ganon:
                    enemy = EnemyList.Aquamentus;
                    break;
            }
        }
    }
}