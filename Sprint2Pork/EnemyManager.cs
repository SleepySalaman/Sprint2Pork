using System;
using System.Collections.Generic;
using System.Linq;
using Sprint2Pork.Entity.Moving;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint2Pork {
    public class EnemyManager {

        List<Fireball> fireballs;

        private bool attacking = true;
        private int distanceTraveled = 0;

        private double timeSinceAttacked = 0.0;
        private double timeBetweenAttacks = 1.0;

        public EnemyManager(int x) {
            fireballs = new List<Fireball>();
            generateFireballs(x);
        }

        public void Update(GameTime gameTime, int x) {
            for(int i = 0; i < fireballs.Count; i++) {
                fireballs[i].Update();
                fireballs[i].Move();
            }
            if (attacking) {
                distanceTraveled++;
            }
            switchModes(gameTime, x);
        }

        public void Draw(SpriteBatch sb, Texture2D txt) {
            for(int i = 0; i < fireballs.Count; i++) {
                fireballs[i].Draw(sb, txt);
            }
        }

        private void switchModes(GameTime gameTime, int x) {
            if (attacking) {
                if(distanceTraveled > 100) {
                    attacking = false;
                    fireballs.Clear();
                    distanceTraveled = 0;
                }
            } else {
                timeSinceAttacked += gameTime.ElapsedGameTime.TotalSeconds;
                if(timeSinceAttacked > timeBetweenAttacks) {
                    timeSinceAttacked = 0.0;
                    attacking = true;
                    generateFireballs(x);
                }

            }
        }

        public void generateFireballs(int x) {
            for(int i = 0; i < 3; i++) {
                fireballs.Add(new Fireball(i, x));
            }
        }

        public void clearFireballs() {
            fireballs.Clear();
        }

    }
}
