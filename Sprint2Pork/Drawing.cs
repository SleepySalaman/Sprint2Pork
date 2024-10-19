using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork {
    public class Drawing {

        public Drawing() {

        }

        public void DrawCyclingEnemy(UpdateEnemySprite enemyUpdater, EnemyManager enemyManager, SpriteBatch spriteBatch, List<Texture2D> allTextures,
            IEnemy enemySprite, int currentEnemyNum, ISprite textSprite) {
            enemyUpdater.drawCurrentEnemy(enemySprite, spriteBatch, allTextures, currentEnemyNum);
            textSprite.Draw(spriteBatch, allTextures[0]);
            enemyManager.Draw(spriteBatch, allTextures[1]);
        }

        public void DrawGeneratedObjects(SpriteBatch spriteBatch, List<Block> blocks, List<GroundItem> groundItems,
            List<IEnemy> enemies, List<EnemyManager> fireballManagers, List<Texture2D> allTextures) {
            foreach (Block block in blocks) {
                block.Draw(spriteBatch);
            }

            foreach (var item in groundItems) {
                item.Draw(spriteBatch, allTextures[9]);
            }

            foreach (var enemy in enemies) {
                enemy.Draw(spriteBatch, allTextures[2]);
            }
            foreach (var fireball in fireballManagers) {
                fireball.Draw(spriteBatch, allTextures[1]);
            }
        }

    }
}
