﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Pork.Constants;

namespace Sprint2Pork {
    public class LinkHealth {

        private int[] linkHealth;

        public LinkHealth() {
            linkHealth = new int[5]{ 0, 0, 0, 0, 0};
        }

        public bool takeDamage() {
            for(int i = 4; i >= 0; i--) {
                if (linkHealth[i] < 2) {
                    linkHealth[i]++;
                    return false;
                }
            }
            return true;
        }

        public void drawLives(SpriteBatch sb, Texture2D txt, Viewport viewport) {
            for(int i = 0; i < 5; i++) {
                sb.Draw(txt, new Rectangle(((viewport.Width * 13) / 21) + (50 * i), GameConstants.HUD_HEIGHT / 3, 50, 50),
                    new Rectangle(210 + (100 * linkHealth[i]), 260, 100, 100), Color.White);
            } 
        }

        public bool linkAlive() {
            return linkHealth[0] != 2;
        }

    }
}