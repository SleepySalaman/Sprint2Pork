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
        private double timeBetweenAttacks = 3.0;

        public EnemyManager() {
            fireballs = new List<Fireball>();
        }

        public void Update(GameTime gameTime) {
            for(int i = 0; i < fireballs.Count; i++) {
                fireballs[i].Update();
            }
            distanceTraveled++;
        }

        public void Draw(SpriteBatch sb, Texture2D txt) {
            for(int i = 0; i < fireballs.Count; i++) {
                fireballs[i].Draw(sb, txt);
            }
        }

        public void switchModes(GameTime gameTime) {
            if (attacking) {
                if(distanceTraveled > 50) {
                    attacking = false;
                    //fireballs.Clear();
                }
            } else {
                timeSinceAttacked += gameTime.ElapsedGameTime.TotalSeconds;
                if(timeSinceAttacked > timeBetweenAttacks) {
                    timeSinceAttacked = 0.0;
                    attacking = true;
                    //generateFireballs();
                }

            }
        }

        public void generateFireballs() {
            for(int i = 0; i < 3; i++) {
                fireballs.Add(new Fireball(i));
            }
        }

    }
}
