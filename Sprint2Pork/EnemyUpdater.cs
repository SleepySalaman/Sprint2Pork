using Microsoft.Xna.Framework;
using Sprint2Pork.Blocks;
using Sprint2Pork.Entity.Moving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork {
    public class EnemyUpdater {

        public static void updateEnemies(ref Link link, List<IEnemy> enemies, List<Block> blocks) {
            foreach (var enemy in enemies) {
                enemy.Update();
                enemy.Move(blocks);
                bool collidesWithLink = Collision.Collides(link.GetRect(), enemy.getRect());

                enemy.updateFromCollision(collidesWithLink, Color.Red);
                if (collidesWithLink) {
                    link.TakeDamage();
                }
            }
        }

        public static void UpdateFireballs(EnemyManager enemyManager, ref Link link, ref List<EnemyManager> fireballManagers,
            GameTime gameTime, ref LinkHealth health) {
            foreach (var fireball in fireballManagers)
            {
                fireball.Update(gameTime, 0);
                foreach (var fireballRect in fireball.GetFireballRects()){
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
