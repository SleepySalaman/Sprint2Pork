using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Entity.Moving;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class EnemyManager
    {

        private int startX, startY;

        List<Fireball> fireballs;

        private bool attacking = true;
        private int distanceTraveled = 0;

        private double timeSinceAttacked = 0.0;
        private double timeBetweenAttacks = 1.0;

        private SoundManager soundManager;

        public EnemyManager(int x, int initX, int initY, SoundManager smparam)
        {
            fireballs = new List<Fireball>();
            startX = initX;
            startY = initY;
            soundManager = smparam;
            generateFireballs(x);
        }

        public void Update(GameTime gameTime, int x)
        {
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update();
                fireballs[i].Move();
            }
            if (attacking)
            {
                distanceTraveled++;
            }
            switchModes(gameTime, x);
        }

        public void Draw(SpriteBatch sb, Texture2D txt, Texture2D hitboxTexture, bool showHitbox)
        {
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Draw(sb, txt, hitboxTexture, txt, showHitbox);
            }
        }

        public List<Rectangle> GetFireballRects()
        {
            List<Rectangle> fireballRects = new List<Rectangle>();
            foreach (var fireball in fireballs)
            {
                fireballRects.Add(fireball.GetRect());
            }
            return fireballRects;
        }

        private void switchModes(GameTime gameTime, int x)
        {
            if (attacking)
            {
                if (distanceTraveled > 100)
                {
                    attacking = false;
                    fireballs.Clear();
                    distanceTraveled = 0;
                }
            }
            else
            {
                timeSinceAttacked += gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceAttacked > timeBetweenAttacks)
                {
                    timeSinceAttacked = 0.0;
                    attacking = true;
                    generateFireballs(x);
                }

            }
        }

        public void generateFireballs(int x)
        {
            soundManager.PlaySound("sfxFlamesShot");
            for (int i = 0; i < 3; i++)
            {
                fireballs.Add(new Fireball(i, x, startX, startY));
            }
        }

        public void clearFireballs()
        {
            fireballs.Clear();
        }

        public List<Fireball> getFireballs()
        {
            return fireballs;
        }

    }
}
