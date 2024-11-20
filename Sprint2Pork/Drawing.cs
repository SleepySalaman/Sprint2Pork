﻿using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity;
using Sprint2Pork.Entity.Moving;
using Sprint2Pork.Items;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class Drawing
    {

        public static void DrawCyclingEnemy(UpdateEnemySprite enemyUpdater, EnemyManager enemyManager, SpriteBatch spriteBatch,
            List<Texture2D> allTextures, IEnemy enemySprite, int currentEnemyNum, ISprite textSprite, Texture2D lifeTxt, Texture2D hitboxTxt, bool showHitbox)
        {
            enemyUpdater.DrawCurrentEnemy(enemySprite, spriteBatch, allTextures, currentEnemyNum, lifeTxt, hitboxTxt, showHitbox);
            textSprite.Draw(spriteBatch, allTextures[0]);
            enemyManager.Draw(spriteBatch, allTextures[1], hitboxTxt, showHitbox);
        }

        public static void DrawGeneratedObjects(SpriteBatch spriteBatch, List<Block> blocks, List<GroundItem> groundItems,
            List<IEnemy> enemies, List<EnemyManager> fireballManagers, List<Texture2D> allTextures, Texture2D lifeTexture,
            Texture2D hitboxTexture, bool showHitbox)
        {
            foreach (Block block in blocks)
            {
                block.Draw(spriteBatch);
            }

            foreach (var item in groundItems)
            {
                item.Draw(spriteBatch, allTextures[9]);
            }

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, allTextures[enemy.getTextureIndex()], lifeTexture, hitboxTexture, showHitbox);
            }
            foreach (var fireball in fireballManagers)
            {
                fireball.Draw(spriteBatch, allTextures[1], hitboxTexture, showHitbox);
            }
        }

    }
}
