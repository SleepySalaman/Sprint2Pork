using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class EnemyUpdater
    {

        public static void UpdateEnemies(Link link, List<IEnemy> enemies, List<Block> blocks, List<EnemyManager> fireballManagers,
            LinkHealth healthCount)
        {
            if (link.IsLinkUsingItem())
            {
                link.linkItem.Update(link);
            }

            var enemiesToRemove = new List<IEnemy>();
            var fireballsToRemove = new List<EnemyManager>();
            foreach (Enemy enemy in enemies)
            {
                enemy.Update();
                enemy.Move(blocks);
                bool collidesWithLink = Collision.Collides(link.GetRect(), enemy.GetRect());
                if (collidesWithLink)
                {
                    link.TakeDamage();
                    healthCount.TakeDamage();
                }
                if (link.IsLinkUsingItem())
                {
                    if (link.linkItem.Collides(enemy.GetRect()))
                    {
                        enemy.TakeDamage();
                    }
                }
                if (enemy.getHealth() <= 0)
                {
                    int id = enemy.GetFireballID();
                    enemiesToRemove.Add(enemy);
                    if (id != 0)
                    {
                        foreach (var manager in fireballManagers)
                        {
                            if (manager.GetID() == id)
                            {
                                fireballsToRemove.Add(manager);
                            }
                        }
                    }
                }
            }
            foreach (var enemy in enemiesToRemove)
            {
                enemies.Remove(enemy);
            }
            foreach (var manager in fireballsToRemove)
            {
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
