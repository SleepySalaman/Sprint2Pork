using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork {
    public class InitializeHandler {

        public static void baseInitialize(ref Viewport viewport, GraphicsDeviceManager graphics, ref Link link,
            ref List<IController> controllerList, ref List<Block> blocks, Game1 game, SoundManager soundManager) {
            viewport = graphics.GraphicsDevice.Viewport;
            link = new Link(viewport.Width, viewport.Height, soundManager);

            controllerList.Add(new KeyboardController(game, link, blocks));
            controllerList.Add(new MouseController(game));
        }

        public static void loadEnemyContent(ref SpriteBatch spriteBatch, ref UpdateEnemySprite enemyUpdater,
            ref EnemyManager enemyManager, GraphicsDevice GraphicsDevice, int enemyInitX,
            int enemyInitY, SoundManager soundManager) {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            enemyUpdater = new UpdateEnemySprite(enemyInitX, enemyInitY);
        }

    }
}
