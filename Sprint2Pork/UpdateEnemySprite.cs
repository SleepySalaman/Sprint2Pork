using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Entity.Moving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork {
    public class UpdateEnemySprite {

        private int enemyInitX, enemyInitY;

        public UpdateEnemySprite(int enemyInitX, int enemyInitY) {
            this.enemyInitX = enemyInitX;
            this.enemyInitY = enemyInitY;
        }

        public void setEnemySprite(int currentEnemyNum, ref IEnemy enemySprite, ref EnemyManager enemyManager) {
            switch (currentEnemyNum) {
                case 0: enemySprite = new Aquamentus(enemyInitX, enemyInitY); enemyManager = new EnemyManager(enemySprite.getX(), enemyInitX, enemyInitY); break;
                case 1: enemySprite = new Dodongo(enemyInitX, enemyInitY); enemyManager.clearFireballs(); break;
                case 2: enemySprite = new Manhandla(enemyInitX, enemyInitY); break;
                case 3: enemySprite = new Gleeok(enemyInitX, enemyInitY); break;
                case 4: enemySprite = new Digdogger(enemyInitX, enemyInitY); break;
                case 5: enemySprite = new Gohma(enemyInitX, enemyInitY); break;
                case 6: enemySprite = new Ganon(enemyInitX, enemyInitY); break;
                case 7: enemySprite = new Gel(enemyInitX, enemyInitY); break;
                case 8: enemySprite = new Bat(enemyInitX, enemyInitY); break;
                case 9: enemySprite = new Goriya(enemyInitX, enemyInitY); break;
                case 10: enemySprite = new Wizard(enemyInitX, enemyInitY); break;
                case 11: enemySprite = new Stalfos(enemyInitX, enemyInitY); enemyManager.clearFireballs(); break;
            }
        }
        
        public void drawCurrentEnemy(IEnemy enemySprite, SpriteBatch spriteBatch, List<Texture2D> allTextures, int currentEnemyNum) {
            if (currentEnemyNum < 7) {
                enemySprite.Draw(spriteBatch, allTextures[2]);
            } else {
                enemySprite.Draw(spriteBatch, allTextures[currentEnemyNum - 4]);
            }
        }

    }
}
