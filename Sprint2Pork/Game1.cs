using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sprint2Pork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ISprite characterSprite;
        private ISprite textSprite;
        private List<IController> controllerList;
        private Texture2D characterTexture;
        private Texture2D enemyTexture;
        private SpriteFont font;
        //private int spriteMode;
        //Hello does this work?
        private int[] spritePos;
        private bool moving;

        public enum PlayerSpriteList { NonMovingNonAnimatedPlayer, NonMovingAnimatedPlayer, MovingNonAnimatedPlayer, MovingAnimatedPlayer };

        private PlayerSpriteList playerMode;

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
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            characterSprite = new NonMovingNonAnimatedSprite(spritePos[0], spritePos[1]);
            characterTexture = Content.Load<Texture2D>("mario");
            enemyTexture = Content.Load<Texture2D>("zeldabosses");
            font = Content.Load<SpriteFont>("File");
            textSprite = new TextSprite(200, 100, font);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.D0) 
                || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Exit();
            }

            foreach (IController c in controllerList)
            {
                c.Update();
            }

            if (moving && spritePos[1] < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2.2)
            {
                spritePos[1]++;
            }
            else if (moving)
            {
                spritePos[1] = -5;
            }

            characterSprite.Update(spritePos[0], spritePos[1]);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Cyan);

            characterSprite.Draw(spriteBatch, characterTexture);
            textSprite.Draw(spriteBatch, characterTexture);

            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void setMode(PlayerSpriteList spriteList){
            if (playerMode != spriteList){
                playerMode = spriteList;
                switch (spriteList){
                    case PlayerSpriteList.NonMovingNonAnimatedPlayer:
                        characterSprite = new NonMovingNonAnimatedSprite(spritePos[0], spritePos[1]);
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