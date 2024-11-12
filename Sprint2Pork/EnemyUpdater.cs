using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class EnemyUpdater
    {

        public static void updateEnemies(Link link, List<IEnemy> enemies, List<Block> blocks, List<EnemyManager> fireballManagers)
        {
            var enemiesToRemove = new List<IEnemy>();
            var fireballsToRemove = new List<EnemyManager>();
            foreach (var enemy in enemies)
            {
                enemy.Update();
                enemy.Move(blocks);
                bool collidesWithLink = Collision.Collides(link.GetRect(), enemy.getRect());
                bool collidesWithLinkItem = link.linkItem.Collides(enemy.getRect());

                enemy.UpdateFromCollision(collidesWithLink, Color.Red);
                enemy.UpdateFromCollision(collidesWithLinkItem, Color.Red);
                if (enemy.getHealth() <= 0)
                {
                    int id = enemy.getFireballID();
                    enemiesToRemove.Add(enemy);
                    if(id != 0) {
                        foreach (var manager in fireballManagers) {
                            if (manager.GetID() == id) {
                                fireballsToRemove.Add(manager);
                            }
                        }
                    }
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
            foreach (var manager in fireballsToRemove) {
                fireballManagers.Remove(manager);
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
                        health.TakeDamage();
                    }
                }
            }
        }

    }
}
