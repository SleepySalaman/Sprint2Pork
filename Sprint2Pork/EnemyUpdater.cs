using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class EnemyUpdater
    {

        public static void updateEnemies(ref Link link, List<IEnemy> enemies, List<Block> blocks)
        {
            var enemiesToRemove = new List<IEnemy>();
            foreach (var enemy in enemies)
            {
                enemy.Update();
                enemy.Move(blocks);
                bool collidesWithLink = Collision.Collides(link.GetRect(), enemy.getRect());

                enemy.updateFromCollision(collidesWithLink, Color.Red);
                if (enemy.getHealth() <= 0)
                {
                    enemiesToRemove.Add(enemy);
                }
                if (collidesWithLink)
                {
                    link.TakeDamage();
                }
            }
            foreach (var enemy in enemiesToRemove)
            {
                enemies.Remove(enemy);
            }
        }

        public static void UpdateFireballs(EnemyManager enemyManager, ref Link link, ref List<EnemyManager> fireballManagers,
            GameTime gameTime, ref LinkHealth health)
        {
            foreach (var fireball in fireballManagers)
            {
                fireball.Update(gameTime, 0);
                foreach (var fireballRect in fireball.GetFireballRects())
                {
                    if (Collision.Collides(link.GetRect(), fireballRect))
                    {
                        link.TakeDamage();
                        health.takeDamage();
                    }
                }
            }
        }

    }
}
