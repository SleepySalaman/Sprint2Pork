using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Constants;

namespace Sprint2Pork
{
    public class LinkHealth
    {

        private int[] linkHealth;

        public LinkHealth()
        {
            linkHealth = new int[5] { 0, 0, 0, 0, 0 };
        }

        public bool TakeDamage()
        {
            for (int i = 4; i >= 0; i--)
            {
                if (linkHealth[i] < 2)
                {
                    linkHealth[i]++;
                    return false;
                }
            }
            return true;
        }

        public void DrawLives(SpriteBatch sb, Texture2D txt, Viewport viewport)
        {
            for (int i = 0; i < 5; i++)
            {
                sb.Draw(txt, new Rectangle(((viewport.Width * 13) / 21) + (50 * i), GameConstants.HUD_HEIGHT / 3, 50, 50),
                    new Rectangle(210 + (100 * linkHealth[i]), 260, 100, 100), Color.White);
            }
        }

        public void HealFullHeart()
        {
            for (int i = 0; i < 5; i++)
            {
                linkHealth[i] = 0;
            }
        }

        public void HealHalfHeart()
        {
            for (int i = 0; i < 5; i++)
            {
                if (linkHealth[i] > 0)
                {
                    linkHealth[i]--;
                    return;
                }
            }
        }

        public bool IsLinkAlive()
        {
            return linkHealth[0] != 2;
        }

    }
}
