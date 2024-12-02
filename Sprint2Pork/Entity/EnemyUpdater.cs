using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using System.Collections.Generic;

namespace Sprint2Pork.Entity
{
    public class EnemyUpdater
    {

        // Add a dictionary to track which enemies have been hit during the current activation
        private static Dictionary<IEnemy, bool> enemyHitTracker = new Dictionary<IEnemy, bool>();


        public static void UpdateEnemies(Link link, List<IEnemy> enemies, List<Block> blocks, List<EnemyManager> fireballManagers, LinkHealth healthCount, GameTime gameTime, float enemyStopTimer, bool isEnemyStopActive)
        {
            if (link.IsLinkUsingItem())
            {
                link.linkItem.Update(link);

                // Reset the tracker for enemies no longer in range
                var enemiesOutOfRange = new List<IEnemy>();
                foreach (var enemy in enemyHitTracker.Keys)
                {
                    if (!link.linkItem.Collides(enemy.GetRect()))
                    {
                        enemiesOutOfRange.Add(enemy);
                    }
                }
                foreach (var enemy in enemiesOutOfRange)
                {
                    enemyHitTracker.Remove(enemy);
                }
            }
            else
            {
                // Clear the tracker when Link's item is not in use
                enemyHitTracker.Clear();
            }

            var enemiesToRemove = new List<IEnemy>();
            var fireballsToRemove = new List<EnemyManager>();

            foreach (Enemy enemy in enemies)
            {
                if (!isEnemyStopActive)
                {
                    enemy.Update();
                    enemy.Move(blocks);
                }

                // Check for collision with Link
                if (Collision.Collides(link.GetRect(), enemy.GetRect()) && !link.isInvincible)
                {
                    link.TakeDamage();
                    healthCount.TakeDamage();
                }

                // Check if Link's item collides with the enemy
                if (link.IsLinkUsingItem() && link.linkItem.Collides(enemy.GetRect()))
                {
                    if (!enemyHitTracker.ContainsKey(enemy))
                    {
                        enemy.TakeDamage();
                        enemyHitTracker[enemy] = true;

                        // Add this check to specifically handle arrows
                        //if (link.linkItem is Arrow || link.linkItem is WoodArrow)
                        //{
                        //    link.LoseItem();
                        //}
                    }
                }

                // Check if the enemy should be removed
                if (enemy.getHealth() <= 0)
                {
                    int fireballID = enemy.GetFireballID();
                    enemiesToRemove.Add(enemy);

                    // Remove corresponding fireball manager if it exists
                    if (fireballID != 0)
                    {
                        foreach (var manager in fireballManagers)
                        {
                            if (manager.GetID() == fireballID)
                            {
                                fireballsToRemove.Add(manager);
                            }
                        }
                    }
                }
            }

            // Remove defeated enemies
            foreach (var enemy in enemiesToRemove)
            {
                // OLD MAN'S CURSE WOULD GO HERE
                if (enemy is Wizard)
                {
 
                }
                enemies.Remove(enemy);
            }

            // Remove fireball managers associated with defeated enemies
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
                    if (Collision.Collides(link.GetRect(), fireballRect) && !link.isInvincible)
                    {
                        link.TakeDamage();
                        health.TakeDamage();
                    }
                }
            }
        }

    }
}
