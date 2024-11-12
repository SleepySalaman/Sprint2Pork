using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Entity.Moving;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class UpdateEnemySprite
    {

        private int enemyInitX, enemyInitY;

        public UpdateEnemySprite(int enemyInitX, int enemyInitY)
        {
            this.enemyInitX = enemyInitX;
            this.enemyInitY = enemyInitY;
        }

        public void DrawCurrentEnemy(IEnemy enemySprite, SpriteBatch spriteBatch, List<Texture2D> allTextures, int currentEnemyNum,
            Texture2D lifeTxt, Texture2D hitboxTxt, bool showHitbox)
        {
            if (currentEnemyNum < 7)
            {
                enemySprite.Draw(spriteBatch, allTextures[2], lifeTxt, hitboxTxt, showHitbox);
            }
            else
            {
                enemySprite.Draw(spriteBatch, allTextures[currentEnemyNum - 4], lifeTxt, hitboxTxt, showHitbox);
            }
        }

    }
}
