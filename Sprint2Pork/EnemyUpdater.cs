using Microsoft.Xna.Framework;
using Sprint2Pork.Entity.Moving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork {
    public class EnemyUpdater {

        public static void updateEnemies(ref Link link, List<IEnemy> enemies) {
            foreach (var enemy in enemies) {
                enemy.Update();
                enemy.Move();
                bool collidesWithLink = Collision.Collides(link.GetRect(), enemy.getRect());

                enemy.updateFromCollision(collidesWithLink, Color.Red);
                if (collidesWithLink) {

                    link.TakeDamage();
                }
            }
        }

        public static void updateFireballs(EnemyManager enemyManager, ref Link link, ref List<EnemyManager> fireballManagers,
            GameTime gameTime) {
            foreach (var fireball in fireballManagers) {
                fireball.Update(gameTime, 0);
            }
            List<Fireball> fireballs = enemyManager.getFireballs();
            foreach (var fireball in fireballs) {
                bool collides = Collision.Collides(link.GetRect(), fireball.getRect());
                if (collides) {
                    link.TakeDamage();
                }
            }
        }

    }
}
