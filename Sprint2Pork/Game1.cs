using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2Pork.Blocks;
using Sprint2Pork.Enemies;
using Sprint2Pork.Enemies.Aquamentus;
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
        private ISprite characterSprite;
        private IEnemy enemySprite;
        private ISprite textSprite;
        private List<IController> controllerList;
        private Texture2D characterTexture;
        private Texture2D linkTexture;
        private Texture2D enemyTexture;
        private SpriteFont font;
        //private int spriteMode;
        //Hello does this work?
        private int[] spritePos;
        private bool moving;
        private double switchCooldown = 0.1;  // 0.5 seconds cooldown between switches
        private double timeSinceLastSwitch = 0;
        // Block-related variables
        private Texture2D blockTexture;
        private List<Block> blocks;
        private int currentBlockIndex;
        private Vector2 blockPosition;  // Constant position for all blocks

        public enum Direction { Left, Right };
        public enum PlayerSpriteList { NonMovingNonAnimatedPlayer, NonMovingAnimatedPlayer, MovingNonAnimatedPlayer, MovingAnimatedPlayer };

        private PlayerSpriteList playerMode;
        private ISprite staticSprite;
        private ISprite animatedSprite;
        private ISprite currentSprite;
        private ISprite linkSprite;

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
            moving = false;

            blocks = new List<Block>();
            currentBlockIndex = 0;
            blockPosition = new Vector2(200, 200);  // Constant position for block

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            characterSprite = new NonMovingNonAnimatedSprite(spritePos[0], spritePos[1], new Rectangle(200, 120, 30, 35));
            enemySprite = new AquamentusNotAttacking();
            staticSprite = new NonMovingNonAnimatedSprite(spritePos[0], spritePos[1], new Rectangle(200, 120, 30, 35));
            animatedSprite = new MovingAnimatedSprite(spritePos[0], spritePos[1]);
            currentSprite = staticSprite;
            characterTexture = Content.Load<Texture2D>("mario");
            enemyTexture = Content.Load<Texture2D>("zeldabosses");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            characterTexture = Content.Load<Texture2D>("mario");
            blockTexture = Content.Load<Texture2D>("blocks");
            linkTexture = Content.Load<Texture2D>("Link_Moving");

            // Create blocks at different positions
            // Define blocks using specific tiles from the sprite sheet
            // Define blocks using different source rectangles (tiles) but same position
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 0, 0, 16, 16)));  // Tile 1
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 1, 0, 16, 16))); // Tile 2
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 2, 0, 16, 16))); // Tile 3
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 3, 0, 16, 16))); // Tile 4
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 4, 0, 16, 16))); // Tile 4
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 5, 0, 16, 16))); // Tile 4
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 6, 0, 16, 16))); // Tile 4
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 7, 0, 16, 16))); // Tile 4
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 8, 0, 16, 16))); // Tile 4
            blocks.Add(new Block(blockTexture, blockPosition, new Rectangle(16 * 9, 0, 16, 16))); // Tile 4
            font = Content.Load<SpriteFont>("File");
            textSprite = new TextSprite(200, 100, font);

            //Link
            link = new Link();
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

            // Handle WASD movement
            if (state.IsKeyDown(Keys.W))
            {
                spritePos[1] -= 5; // Move up
            }
            if (state.IsKeyDown(Keys.S))
            {
                spritePos[1] += 5; // Move down
            }
            if (state.IsKeyDown(Keys.A))
            {
                spritePos[0] -= 5; // Move left
                isMoving = true;
            }
            if (state.IsKeyDown(Keys.D))
            {
                spritePos[0] += 5; // Move right
                isMoving = true;
            }

            // Switch between static and animated sprites
            if (isMoving)
            {
                currentSprite = animatedSprite;
            }
            else
            {
                currentSprite = staticSprite;
            }

            // Update the current sprite
            currentSprite.Update(spritePos[0], spritePos[1]);
            enemySprite.Update();

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
            if (state.IsKeyDown(Keys.T) && timeSinceLastSwitch >= switchCooldown)
            {
                currentBlockIndex = (currentBlockIndex - 1 + blocks.Count) % blocks.Count;
                timeSinceLastSwitch = 0;
            }

            // Switch to the next block with 'Y' and cooldown check
            if (state.IsKeyDown(Keys.Y) && timeSinceLastSwitch >= switchCooldown)
            {
                currentBlockIndex = (currentBlockIndex + 1) % blocks.Count;
                timeSinceLastSwitch = 0;
            }

            base.Update(gameTime);
        }

        private void ResetGame()
        {
            // Reset block index to the first block
            currentBlockIndex = 0;

            // Reset character position
            spritePos[0] = 50;
            spritePos[1] = 50;

            // Reset other necessary variables
            moving = false;
            timeSinceLastSwitch = 0;

            // Reset the character sprite to its initial mode (NonMovingNonAnimatedSprite)
            setMode(PlayerSpriteList.NonMovingNonAnimatedPlayer);
            enemySprite = new AquamentusNotAttacking();
        }


        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentSprite.Draw(spriteBatch, characterTexture);
            enemySprite.Draw(spriteBatch, enemyTexture);
            textSprite.Draw(spriteBatch, characterTexture);
            link.Draw(spriteBatch, linkTexture);
            KeyboardState state = Keyboard.GetState();
            bool isMoving = false;
            Vector2 newPosition = new Vector2(spritePos[0], spritePos[1]);

            // Handle WASD movement
            if (state.IsKeyDown(Keys.W))
            {
                newPosition.Y -= 1; // Move up
                isMoving = true;
            }
            if (state.IsKeyDown(Keys.S))
            {
                newPosition.Y += 1; // Move down
                isMoving = true;
            }
            if (state.IsKeyDown(Keys.A))
            {
                newPosition.X -= 1; // Move left
                isMoving = true;
            }
            if (state.IsKeyDown(Keys.D))
            {
                newPosition.X += 1; // Move right
                isMoving = true;
            }

            // Switch between static and animated sprites
            currentSprite = isMoving ? animatedSprite : staticSprite;

            // Update the current sprite only if the position has changed
            if (newPosition != new Vector2(spritePos[0], spritePos[1]))
            {
                spritePos[0] = (int)newPosition.X;
                spritePos[1] = (int)newPosition.Y;
                currentSprite.Update(spritePos[0], spritePos[1]);
            }

            // Existing code for quitting with Escape or Mouse click
            // Draw the current block
            blocks[currentBlockIndex].Draw(spriteBatch);

            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void setMode(PlayerSpriteList spriteList){
            if (playerMode != spriteList){
                playerMode = spriteList;
                switch (spriteList){
                    case PlayerSpriteList.NonMovingNonAnimatedPlayer:
                        characterSprite = new NonMovingNonAnimatedSprite(spritePos[0], spritePos[1], new Rectangle(200, 120, 30, 35));
                        moving = false;
                        break;
                    case PlayerSpriteList.NonMovingAnimatedPlayer:
                        characterSprite = new NonMovingAnimatedSprite(spritePos[0], spritePos[1]);
                        moving = false;
                        break;
                    case PlayerSpriteList.MovingNonAnimatedPlayer:
                        characterSprite = new MovingNonAnimatedSprite(spritePos[0], spritePos[1]);
                        moving = true;
                        break;
                    case PlayerSpriteList.MovingAnimatedPlayer:
                        characterSprite = new MovingAnimatedSprite(spritePos[0], spritePos[1]);
                        moving = true;
                        break;
                }
            }
        }
    }
}