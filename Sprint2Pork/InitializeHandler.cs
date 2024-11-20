using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity;
using Sprint2Pork.Essentials;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class InitializeHandler
    {

        public static void BaseInitialize(ref Viewport viewport, GraphicsDeviceManager graphics, ref Link link,
            ref List<IController> controllerList, ref List<Block> blocks, Game1 game, SoundManager soundManager, Inventory inventory)
        {
            viewport = graphics.GraphicsDevice.Viewport;
            link = new Link(viewport.Width, viewport.Height, soundManager, inventory);

            controllerList.Add(new KeyboardController(game, link, blocks));
            controllerList.Add(new MouseController(game));
        }

        public static void LoadEnemyContent(ref SpriteBatch spriteBatch, ref UpdateEnemySprite enemyUpdater,
            ref EnemyManager enemyManager, GraphicsDevice GraphicsDevice, int enemyInitX,
            int enemyInitY, SoundManager soundManager)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            enemyUpdater = new UpdateEnemySprite(enemyInitX, enemyInitY);
        }
    }
}
