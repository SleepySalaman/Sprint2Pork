using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Constants;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Entity;
using Sprint2Pork.Essentials;
using Sprint2Pork.Items;
using Sprint2Pork.Managers;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class GameStateManager
    {
        private  Game1 game;
        private  Link link;
        private  SoundManager soundManager;
        private  Inventory inventory;
        private  LinkHealth healthCount;
        private  List<IController> controllerList;
        private  Minimap minimap;
        private  HUD hud;
        private  GraphicsDevice graphicsDevice;

        public GameStateManager(Game1 game, Link link, SoundManager soundManager,
            Inventory inventory, LinkHealth healthCount, List<IController> controllerList,
            Minimap minimap, HUD hud, GraphicsDevice graphicsDevice)
        {
            this.game = game;
            this.link = link;
            this.soundManager = soundManager;
            this.inventory = inventory;
            this.healthCount = healthCount;
            this.controllerList = controllerList;
            this.minimap = minimap;
            this.hud = hud;
            this.graphicsDevice = graphicsDevice;
        }


        public void ResetGame()
        {
            // Re-initialize game components
            game.InitializeGameComponents();

            game.link = link;

            // Update Link reference in all controllers
            foreach (IController controller in controllerList)
            {
                if (controller is KeyboardController keyboardController)
                {
                    keyboardController.UpdateLink(link);
                }
            }
            // Set initial states and flags
            game.link.directionState = new DownFacingLinkState(game.link);
            game.link.actionState = new IdleActionState(game.link);
            game.link.linkItem = new NoItem();
            game.link.isInvincible = false;
            game.link.SetX(115);
            game.link.SetY(180);

            // Reset other game components
            game.spritePos[0] = GameConstants.DEFAULT_SPRITE_POSITION;
            game.spritePos[1] = GameConstants.DEFAULT_SPRITE_POSITION;
            game.enemyUpdater = new UpdateEnemySprite((int)game.enemyInitPos.X, (int)game.enemyInitPos.Y);
            game.soundManager = new SoundManager();
            game.soundManager.LoadAllSounds(game.Content);
            game.inventory.Reset();
            game.healthCount = new LinkHealth();
            game.hud.SubscribeToLinkEvents(game.link);
            game.minimap = new Minimap(game.GraphicsDevice, game.link);
            game.currentRoom = "room1";
            game.roomTexture = game.roomManager.GetNextRoomTexture(game.currentRoom);
            (game.blocks, game.groundItems, game.enemies, game.fireballManagers) = game.rooms[game.currentRoom];
            game.SetGameState(Game1State.Playing);
        }



        public void GameOver()
        {
            game.SetGameState(Game1State.GameOver);
        }


        public void TogglePause()
        {
            game.SetGameState(game.gameState == Game1State.Playing ? Game1State.Paused : Game1State.Playing);
        }

        public void ToggleBackgroundMusic()
        {
            soundManager.ToggleBackgroundMusic();
        }


        public void StartGame()
        {
            game.SetGameState(game.gameState == Game1State.StartScreen ? Game1State.Playing : Game1State.StartScreen);
        }
    }
}
